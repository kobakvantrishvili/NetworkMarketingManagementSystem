using FluentValidation;
using NetworkMarketingManagementSystem.Models.Requests.ForSale;

namespace NetworkMarketingManagementSystem.Infrastructure.Validations
{
    public class SaleCreateRequestValidator : AbstractValidator<SaleCreateRequest>
    {
        public SaleCreateRequestValidator()
        {
            RuleFor(x => x.DistributorId)
                .NotEmpty()
                .WithMessage(nameof(SaleCreateRequest.DistributorId) + " must not be empty");

            RuleFor(x => x.SaleDate)
                .NotEmpty()
                .WithMessage("Sale Date must not be empty");

            RuleForEach(x => x.SoldProductsInfo)
                .NotEmpty()
                .WithMessage("Product Information must not be empty")
                .Must(y => y.ProductId != 0)
                .WithMessage("Product Id must not be empty")
                .Must(y => y.Quantity != 0)
                .WithMessage("Product Quantity must not be empty");
        }
    }
}
