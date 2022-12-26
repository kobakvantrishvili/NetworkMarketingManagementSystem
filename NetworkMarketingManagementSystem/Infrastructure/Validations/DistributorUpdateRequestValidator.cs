using FluentValidation;
using NetworkMarketingManagementSystem.Models.Requests.ForDistributor;


namespace NetworkMarketingManagementSystem.Infrastructure.Validations
{
    public class DistributorUpdateRequestValidator : AbstractValidator<DistributorUpdateRequest>
    {

        public DistributorUpdateRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(nameof(DistributorUpdateRequest.FirstName) + " must not be empty");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(nameof(DistributorUpdateRequest.LastName) + " must not be empty");

            RuleFor(x => x.Birthday)
                .NotEmpty()
                .WithMessage(nameof(DistributorUpdateRequest.Birthday) + " must not be empty");

            RuleFor(x => x.Sex)
                .NotEmpty()
                .WithMessage(nameof(DistributorUpdateRequest.Sex) + " must not be empty");

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
                .WithMessage(nameof(DistributorUpdateRequest.IdentityDocument.ReleaseDate) + "field must not be empty");

            RuleFor(x => x.IdentityDocument.ValidUntil)
                .NotEmpty()
                .WithMessage(nameof(DistributorUpdateRequest.IdentityDocument.ValidUntil) + "field must not be empty");

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
