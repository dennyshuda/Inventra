using Inventra.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Repositories.Product;

public class ProductRepository : IProductRepository
{

    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<Models.Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Models.Product?> GetProductByIdAsync(Guid id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Models.Product> CreateProductAsync(Models.Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
}
