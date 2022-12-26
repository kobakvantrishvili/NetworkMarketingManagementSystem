using FluentValidation;
using NetworkMarketingManagementSystem.Domain.ForDistributor;
using NetworkMarketingManagementSystem.Models.Requests.ForDistributor;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace NetworkMarketingManagementSystem.Infrastructure.Validations
{
    public class DistributorCreateRequestValidator : AbstractValidator<DistributorCreateRequest>
    {
        public DistributorCreateRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(nameof(DistributorCreateRequest.FirstName) + " must not be empty");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(nameof(DistributorCreateRequest.LastName) + " must not be empty");

            RuleFor(x => x.Birthday)
                .NotEmpty()
                .WithMessage(nameof(DistributorCreateRequest.Birthday) + " must not be empty");

            RuleFor(x => x.Sex)
                .NotEmpty()
                .WithMessage(nameof(DistributorCreateRequest.Sex) + " must not be empty");

            RuleFor(x => x.IdentityDocument.Type)
                .NotEmpty()
                .WithMessage("Identity Document Type must not be empty");

            RuleFor(x => x.IdentityDocument.Series)
                .MaximumLength(10) 
                .WithMessage("Identity Document Series must be maxumum 10 characters long");

            RuleFor(x => x.IdentityDocument.Number)
                .MaximumLength(10)
                .WithMessage("Identity Document Number must be maxumum 10 characters long");

            RuleFor(x => x.IdentityDocument.ReleaseDate)
                .NotEmpty()
                .WithMessage(nameof(DistributorCreateRequest.IdentityDocument.ReleaseDate) + "field must not be empty");

            RuleFor(x => x.IdentityDocument.ValidUntil)
                .NotEmpty()
                .WithMessage(nameof(DistributorCreateRequest.IdentityDocument.ValidUntil) + "field must not be empty");

            RuleFor(x => x.IdentityDocument.PersonalNumber)
                .NotEmpty()
                .WithMessage("Personal Number must not be empty")
                .MaximumLength(50)
                .WithMessage("Personal Number must be maxumum 50 characters long");

            RuleFor(x => x.IdentityDocument.IssuingAgency)
                .MaximumLength(50)
                .WithMessage("Issuing Agency Name must be maxumum 100 characters long");

            RuleFor(x => x.ContactInfo.Type)
                .NotEmpty()
                .WithMessage("Contact Information Type must not be empty");

            RuleFor(x => x.ContactInfo.Contact)
                .NotEmpty()
                .WithMessage("Contact Information must not be empty")
                .MaximumLength(50)
                .WithMessage("Contact Information must be maximum 100 characters long");

            RuleFor(x => x.AddressInfo.Type)
                .NotEmpty()
                .WithMessage("Address Information Type must not be empty");

            RuleFor(x => x.AddressInfo.Address)
                .NotEmpty()
                .WithMessage("Address Information must not be empty")
                .MaximumLength(50)
                .WithMessage("Address Information must be maximum 100 characters long");
        }
    }
}
