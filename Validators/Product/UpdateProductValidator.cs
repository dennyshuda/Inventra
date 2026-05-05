using FluentValidation;
using Inventra.DTOs.Product;
using Inventra.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Validators.Product;

public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
{
    private readonly AppDbContext _context;

    public UpdateProductValidator(AppDbContext context)
    {
        _context = context;

        RuleFor(p => p.Name)
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.").When(p => p.Name != null);

        RuleFor(p => p.Sku)
            .MinimumLength(3).WithMessage("SKU must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("SKU cannot exceed 50 characters.").When(p => p.Sku != null);

        RuleFor(p => p.PurchasePrice)
            .GreaterThan(0).WithMessage("Purchase price must be greater than 0.").When(p => p.PurchasePrice.HasValue);

        // RuleFor(p => p.SellingPrice)
        //     .GreaterThan(0).WithMessage("Selling price must be greater than 0.");

        // RuleFor(p => p.Stock)
        //     .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");

        // RuleFor(p => p.CategoryId)
        //     .GreaterThan(0).WithMessage("Category ID must be greater than 0.");

        // RuleFor(p => new { p.PurchasePrice, p.SellingPrice })
        //     .Must(x => !x.PurchasePrice.HasValue || !x.SellingPrice.HasValue || x.SellingPrice >= x.PurchasePrice)
        //     .WithMessage("Selling price must be greater than or equal to purchase price.")
        //     .WithName("SellingPrice");
    }
}
