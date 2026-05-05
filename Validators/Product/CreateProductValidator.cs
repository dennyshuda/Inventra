using FluentValidation;
using Inventra.DTOs.Product;
using Inventra.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Validators.Product;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    private readonly AppDbContext _context;

    public CreateProductValidator(AppDbContext context)
    {
        _context = context;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(p => p.Sku)
            .NotEmpty().WithMessage("SKU is required.")
            .MinimumLength(3).WithMessage("SKU must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("SKU cannot exceed 50 characters.");

        RuleFor(p => p.PurchasePrice)
            .GreaterThan(0).WithMessage("Purchase price must be greater than 0.");

        RuleFor(p => p.SellingPrice)
            .GreaterThan(0).WithMessage("Selling price must be greater than 0.");

        RuleFor(p => p.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");

        RuleFor(p => p.CategoryId)
            .GreaterThan(0).WithMessage("Category ID is required.")
            .MustAsync(CategoryExists).WithMessage("Category not found.");

        RuleFor(p => new { p.PurchasePrice, p.SellingPrice })
            .Must(x => x.SellingPrice >= x.PurchasePrice)
            .WithMessage("Selling price must be greater than or equal to purchase price.")
            .WithName("SellingPrice");
    }

    private async Task<bool> CategoryExists(int categoryId, CancellationToken cancellationToken)
    {
        return await _context.Categories.AnyAsync(c => c.Id == categoryId, cancellationToken);
    }
}
