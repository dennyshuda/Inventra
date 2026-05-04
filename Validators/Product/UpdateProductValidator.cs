using FluentValidation;
using Inventra.DTOs.Product;

namespace Inventra.Validators.Product;

public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductValidator()
    {
        RuleFor(p => p.Name)
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.")
            .When(p => !string.IsNullOrEmpty(p.Name));

        RuleFor(p => p.Sku)
            .MinimumLength(3).WithMessage("SKU must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("SKU cannot exceed 50 characters.")
            .When(p => !string.IsNullOrEmpty(p.Sku));

        RuleFor(p => p.PurchasePrice)
            .GreaterThan(0).WithMessage("Purchase price must be greater than 0.")
            .When(p => p.PurchasePrice.HasValue);

        RuleFor(p => p.SellingPrice)
            .GreaterThan(0).WithMessage("Selling price must be greater than 0.")
            .When(p => p.SellingPrice.HasValue);

        RuleFor(p => p.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.")
            .When(p => p.Stock.HasValue);

        RuleFor(p => new { p.PurchasePrice, p.SellingPrice })
            .Must(x => !x.PurchasePrice.HasValue || !x.SellingPrice.HasValue || x.SellingPrice >= x.PurchasePrice)
            .WithMessage("Selling price must be greater than or equal to purchase price.")
            .WithName("SellingPrice");
    }
}
