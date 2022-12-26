using FluentValidation;
using NetworkMarketingManagementSystem.Models.Requests.ForProduct;

namespace NetworkMarketingManagementSystem.Infrastructure.Validations
{
    public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
    {
        public ProductUpdateRequestValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage(nameof(ProductUpdateRequest.Code) + " must not be empty");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(nameof(ProductUpdateRequest.Name) + " must not be empty");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage(nameof(ProductUpdateRequest.Price) + " must not be empty");
        }
    }
}
