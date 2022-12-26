using Mapster;
using Microsoft.EntityFrameworkCore;
using NetworkMarketingManagementSystem.Application.Abstraction;
using NetworkMarketingManagementSystem.Application.Models;
using NetworkMarketingManagementSystem.Application.Models.Enums;
using NetworkMarketingManagementSystem.Domain;
using NetworkMarketingManagementSystem.Domain.ForDistributor;
using NetworkMarketingManagementSystem.Persistence;
using NetworkMarketingManagementSystem.Persistence.Repositories.Abstraction;
using NetworkMarketingManagementSystem.Persistence.Repositories.Implementation;

namespace NetworkMarketingManagementSystem.Application.Implementation
{
    public class DistributorService : IDistributorService
    {
        private readonly IDistributorRepository _distributorRepository;
        private readonly ISaleRepository _saleRepository;

        public DistributorService(IDistributorRepository distributorRepository, ISaleRepository saleRepository)
        {
            _distributorRepository = distributorRepository;
            _saleRepository = saleRepository;
        }

        public async Task<(Status, DistributorServiceModel?)> ReadDistributorAsync(int Id) // No Tracking
        {
            var distributorExists = await _distributorRepository.Exists(x => x.Id == Id);
            if (!distributorExists)
                return (Status.NotFound, null);

            return (Status.Success, (await _distributorRepository.ReadNoTrackingAsync(Id)).Adapt<DistributorServiceModel>());
        }

        public async Task<(Status, List<DistributorServiceModel>?)> ReadAllDistributorAsync() // No Tracking
        {
            var distributorList = (await _distributorRepository.ReadAllNoTrackingAsync()).Adapt<List<DistributorServiceModel>>();
            if (!distributorList.Any())
                return (Status.NotFound, null);

            //List<DistributorServiceModel> convertedDistributorList = new List<DistributorServiceModel>();

            //foreach (var distributor in distributorList)
            //{
            //    convertedDistributorList.Add(distributor.Adapt<DistributorServiceModel>());
            //}

            return (Status.Success, distributorList);
        }

        public async Task<(Status, int?)> CreateDistributorAsync(DistributorServiceModel distributor)
        {
            var distributorExists = await _distributorRepository.Exists(x => x.IdentityDocument.PersonalNumber == distributor.IdentityDocument.PersonalNumber);
            if (distributorExists)
                return (Status.Conflict, null);


            distributor.References = 0; // new distributor won't have any references
            // If distributor wasn't reffered by anyone then he's on the first level of hierarchy
     

            if (distributor.ReferredBy == null || distributor.ReferredBy == 0)
                distributor.Level = 1;
            else // else, distributor will be one level "lower" in hierarchy
            {
                var RefBy = await _distributorRepository.ReadAsync(distributor.ReferredBy.Value);
                if (RefBy is not null)
                {
                    if (RefBy?.Level >= 5 || RefBy?.References >= 3) // If person who refeered our distributor is on 5th level of hierarchy or has already referred 3 people then we don't create new distributor
                        return (Status.Forbidden, null);

                    // We update number of references of the person who referred new distributor
                    RefBy.References += 1;
                    //_context.Entry(RefBy.Adapt<Distributor>()).State = EntityState.Detached;
                    await _distributorRepository.UpdateAsync(RefBy);
                    // setting new distributor level
                    distributor.Level = (int)RefBy?.Level + 1;
                }
                else
                    distributor.Level = 1;
            }

            var distributorId = await _distributorRepository.CreateAsync(distributor.Adapt<Distributor>());
            return (Status.Created, distributorId);
        }

        public async Task<Status> DeleteDistributorAsync(int Id)
        {
            var distributor = await _distributorRepository.ReadNoTrackingAsync(Id);
            if (distributor is null)
                return Status.NotFound;

            if (distributor?.ReferredBy != null) // decrease the references of the person who referenced the distributor that is to be deleted
            {
                var RefBy = await _distributorRepository.ReadAsync((int)distributor.ReferredBy);
                if (RefBy is not null)
                {
                    RefBy.References -= 1;
                    await _distributorRepository.UpdateAsync(RefBy);
                }
            }
            if (distributor?.References != 0) // make RefferedBy null for those distributors who were reffered by the distributor that is to be deleted
                await SetLevelsHelper(distributor, Iteration.Initail);

            // Because we have Foreign Key constraint on DistributorID in Sale Table we have to Change the DistributorID to NULL since Dstributor whom this Id belogs is getting deleted
            /*var salesToBeUpdated = await _saleRepository.ReadAsync(x => x.DistributorId == Id);
            foreach(var saleToBeUpdated in salesToBeUpdated)
            {
                saleToBeUpdated.DistributorId = null;
                await _saleRepository.UpdateAsync(saleToBeUpdated);
            }*/

            await _distributorRepository.DeleteAsync(Id);
            return Status.Success;
        }

        public async Task<Status> UpdateDistributorAsync(DistributorServiceModel distributor)
        {
            var distr = await _distributorRepository.ReadAsync(distributor.Id);
            if (distr is null)
                return Status.NotFound;

            distributor.References = distr.References;
            distributor.Level = distr.Level;

            //If ReferredBy changed
            if (distr?.ReferredBy != distributor.ReferredBy)
            {
                if (distributor.ReferredBy == null)
                {
                    distributor.Level = 1;
                    await SetLevelsHelper(distributor.Adapt<Distributor>(), Iteration.NotInitial);
                }
                else if (await CheckForCircularReference(distributor.Adapt<Distributor>(), distributor.ReferredBy.Value))
                    return Status.BadRequest;
                else
                {
                    var RefBy = await _distributorRepository.ReadAsync((int)distributor.ReferredBy);
                    if (RefBy is not null)
                    {

                        int LowestLevel = await FindLowestLevelHelper(distributor.Adapt<Distributor>(), distributor.Level);
                        if (RefBy?.Level + LowestLevel - distributor.Level >= 5 || RefBy?.References >= 3) // If Level of hierarchy will be greater than 5 or if distributor who referred distributor that we are updating has more than 3 references, don't update
                            return Status.Forbidden;

                        // We update number of references of the person who referred our distributor
                        RefBy.References += 1;
                        await _distributorRepository.UpdateAsync(RefBy);
                        // updating our distributor level
                        distributor.Level = RefBy.Level + 1;
                        // updating rest of the hierarchy below our distributor
                        await SetLevelsHelper(distributor.Adapt<Distributor>(), Iteration.NotInitial);
                    }
                    else
                        return Status.BadRequest;
                }
            }


            distr = Cast(distr, distributor);

            await _distributorRepository.UpdateAsync(distr);
            return Status.Success;
        }

        // HELPER METHODS
        public async Task SetLevelsHelper(Distributor distributor, Iteration i)
        {
            var references = (await _distributorRepository.ReadAsync(x => x.ReferredBy == distributor.Id));

            if (!references.Any())
                return;


            if (i == Iteration.Initail)
            {
                foreach (var reference in references)
                {
                    reference.ReferredBy = null;
                    reference.Level = 1;
                    await _distributorRepository.UpdateAsync(reference);
                    await SetLevelsHelper(reference, Iteration.NotInitial);
                }
            }
            else
            {
                foreach (var reference in references)
                {
                    reference.Level = distributor.Level + 1;
                    await _distributorRepository.UpdateAsync(reference);
                    await SetLevelsHelper(reference, Iteration.NotInitial);
                }
            }
        }

        public async Task<int> FindLowestLevelHelper(Distributor distributor, int lowestLevel)
        {
            var references = await _distributorRepository.ReadNoTrackingAsync(x => x.ReferredBy == distributor.Id);

            if (!references.Any())
                return lowestLevel;

            foreach (var reference in references)
            {
                var returnedLevel = await FindLowestLevelHelper(reference, reference.Level);
                if (returnedLevel > lowestLevel)
                    lowestLevel = returnedLevel;
            }
            return lowestLevel;
        }

        private async Task<bool> CheckForCircularReference(Distributor distributor, int referredBy)
        {
            var references = await _distributorRepository.ReadNoTrackingAsync(x => x.ReferredBy == distributor.Id);

            if (!references.Any())
                return false;

            foreach (var reference in references)
            {
                if(reference.Id == referredBy)
                    return true;
                var returnedLevel = await CheckForCircularReference(reference, referredBy);
            }
            return false;
        }

        private static Distributor Cast(Distributor distributor, DistributorServiceModel model)
        {
            distributor.FirstName = model.FirstName;
            distributor.LastName = model.LastName;
            distributor.Birthday = model.Birthday;
            distributor.Sex = model.Sex;
            distributor.Image = model.Image;
            distributor.ReferredBy = model.ReferredBy;
            distributor.References = model.References;
            distributor.Level = model.Level;
            distributor.IdentityDocument.Type = model.IdentityDocument.Type;
            distributor.IdentityDocument.Series = model.IdentityDocument.Series;
            distributor.IdentityDocument.Number = model.IdentityDocument.Number;
            distributor.IdentityDocument.ReleaseDate = model.IdentityDocument.ReleaseDate;
            distributor.IdentityDocument.ValidUntil = model.IdentityDocument.ValidUntil;
            distributor.IdentityDocument.PersonalNumber = model.IdentityDocument.PersonalNumber;
            distributor.IdentityDocument.IssuingAgency = model.IdentityDocument.IssuingAgency;
            distributor.ContactInfo.Type = model.ContactInfo.Type;
            distributor.ContactInfo.Contact = model.ContactInfo.Contact;
            distributor.AddressInfo.Type = model.AddressInfo.Type;
            distributor.AddressInfo.Address = model.AddressInfo.Address;

            return distributor;
        }

    }
}
