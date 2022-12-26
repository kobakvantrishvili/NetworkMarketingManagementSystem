using Mapster;
using NetworkMarketingManagementSystem.Application.Abstraction;
using NetworkMarketingManagementSystem.Application.Models;
using NetworkMarketingManagementSystem.Application.Models.Enums;
using NetworkMarketingManagementSystem.Persistence.MongoDb.Models;
using NetworkMarketingManagementSystem.Persistence.MongoDb.Repositories.Abstraction;
using NetworkMarketingManagementSystem.Persistence.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Application.Implementation
{
    public class BonusService : IBonusService
    {
        private readonly IBonusRepository _bonusRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IDistributorRepository _distributorRepository;

        public BonusService(IBonusRepository bonusRepository, ISaleRepository saleRepository, IDistributorRepository distributorRepository)
        {
            _bonusRepository = bonusRepository;
            _saleRepository = saleRepository;
            _distributorRepository = distributorRepository;
        }

        public async Task<Status> CreateBonusesAsync(DateTime startDate, DateTime endDate)
        {
            var sales = await _saleRepository.ReadNoTrackingAsync(x => (x.SaleDate >= startDate) && (x.SaleDate <= endDate));
            var bonuses = await _bonusRepository.ReadAsync();

            var salesToCount = sales.Where(x => !((bonuses.SelectMany(a => a.SaleIds).ToList()).Contains(x.Id))).Select(x => x).ToList();
            if(!salesToCount.Any())
                return Status.NotFound;

            var distributorIds = salesToCount.Select(x => x.DistributorId).Distinct().ToList();

            List<Bonus> bonusesToAdd = new List<Bonus>();
            foreach (var distributorId in distributorIds)
            {

                if (!(await _distributorRepository.Exists(x => x.Id == distributorId)))
                    continue;

                var distributor = await _distributorRepository.ReadNoTrackingAsync(distributorId.Value);

                decimal referencesTotalSales = 0;
                decimal secondOrderReferencesTotalSales = 0;

                List<int> referencesIds = (await _distributorRepository.ReadNoTrackingAsync(x => x.ReferredBy == distributorId)).Select(x => x.Id).ToList();
                var salesIds = salesToCount.Where(x => x.DistributorId == distributorId).Select(x => x.Id).ToList();
                decimal totalsales = salesToCount.Where(x => x.DistributorId == distributorId).Select(x => x.TotalPrice).Sum();

                foreach (var referenceId in referencesIds)
                {
                    List<int> SecondOrderReferencesIds = ((await _distributorRepository.ReadNoTrackingAsync(x => x.ReferredBy == referenceId)).Select(x => x.Id).ToList());
                    referencesTotalSales += salesToCount.Where(x => x.DistributorId == referenceId).Select(x => x.TotalPrice).Sum();

                    foreach (var secondOrderReferenceId in SecondOrderReferencesIds)
                    {
                        secondOrderReferencesTotalSales += salesToCount.Where(x => x.DistributorId == secondOrderReferenceId).Select(x => x.TotalPrice).Sum();
                    }
                }

                var BonusAmount = (decimal)((double)totalsales * 0.1 + (double)referencesTotalSales * 0.05 + (double)secondOrderReferencesTotalSales * 0.01);
                Bonus bonus = new Bonus
                {
                    DistributorId = distributorId.Value,
                    DistributorFirstName = distributor.FirstName,
                    DistributorLastName = distributor.LastName,
                    SaleIds = salesIds,
                    StartDate = startDate,
                    EndDate = endDate,
                    Amount = BonusAmount,
                };
                bonusesToAdd.Add(bonus);
            }

            if (!bonusesToAdd.Any())
                return Status.NotFound;

            await _bonusRepository.CreateAsync(bonusesToAdd);
            return (Status.Success);
        }

        public async Task<Status> DeleteBonusesAsync(DateTime startDate, DateTime endDate)
        {
            var bonuses = await _bonusRepository.ReadAsync(x => (x.StartDate >= startDate) && (x.EndDate <= endDate));
            if (!bonuses.Any())
                return Status.NotFound;

            await _bonusRepository.DeleteAsync(x => (x.StartDate >= startDate) && (x.EndDate <= endDate));
            return (Status.Success);
        }

        public async Task<(Status, IQueryable<BonusServiceModel>)> ReadAllBonusesAsync()
        {
            var bonuses = (await _bonusRepository.ReadAsync()).Adapt<List<BonusServiceModel>>();

            if (!bonuses.Any())
                return (Status.NotFound, null);


            return (Status.Success, bonuses.AsQueryable());
        }

        public async Task<(Status, BonusServiceModel)> ReadBonusAsync(string Id)
        {
            var bonus = ((await _bonusRepository.ReadAsync(x => x.Id == Id)).Adapt<List<BonusServiceModel>>());
            if (!bonus.Any())
                return (Status.NotFound, null);


            return (Status.Success, bonus.First());
        }
    }
}
