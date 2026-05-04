using Inventra.Data;
using Inventra.DTOs.Product;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Repositories.Product;

public class ProductRepository : IProductRepository
{

    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<ProductDto>> GetProductsAsync()
    {
        return await _context.Products
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Sku = p.Sku,
                PurchasePrice = p.PurchasePrice,
                SellingPrice = p.SellingPrice,
                Stock = p.Stock,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<ProductDto?> GetProductByIdAsync(Guid id)
    {
        return await _context.Products
            .Where(p => p.Id == id)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Sku = p.Sku,
                PurchasePrice = p.PurchasePrice,
                SellingPrice = p.SellingPrice,
                Stock = p.Stock,
                CreatedAt = p.CreatedAt
            })
            .FirstOrDefaultAsync();
    }
}
