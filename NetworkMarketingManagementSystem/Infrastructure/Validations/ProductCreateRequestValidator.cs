using FluentValidation;
using NetworkMarketingManagementSystem.Models.Requests.ForProduct;

namespace NetworkMarketingManagementSystem.Infrastructure.Validations
{
    public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequest>
    {
        public ProductCreateRequestValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage(nameof(ProductCreateRequest.Code) + " must not be empty");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(nameof(ProductCreateRequest.Name) + " must not be empty");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage(nameof(ProductCreateRequest.Price) + " must not be empty");
        }
    }
}
