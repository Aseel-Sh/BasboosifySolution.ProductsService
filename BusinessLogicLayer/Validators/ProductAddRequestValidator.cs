using Basboosify.BusinessLogicLayer.DTO;
using FluentValidation;

namespace Basboosify.BusinessLogicLayer.Validators;

public class ProductAddRequestValidator : AbstractValidator<ProductAddRequest>
{
    public ProductAddRequestValidator()
    {
        //ProductName
        RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product Name can't be empty");

        //category
        RuleFor(x => x.Category).IsInEnum().WithMessage("Category can't be empty");

        //unitprice
        RuleFor(x => x.UnitPrice).InclusiveBetween(0, double.MaxValue).WithMessage($"Unit Price should be between 0 and {double.MaxValue}.");

        //quantityInStock
        RuleFor(x => x.QuantityInStock).InclusiveBetween(0, int.MaxValue).WithMessage($"Unit Price should be between 0 and {int.MaxValue}.");
    }
}
