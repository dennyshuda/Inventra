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
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<Models.Product?> GetProductByIdAsync(Guid productId)
    {
        return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == productId);
    }
    public async Task<Models.Product> CreateProductAsync(Models.Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == product.Id) ?? product;
    }

    public async Task<Models.Product> UpdateProductAsync(Models.Product product)
    {
        await _context.SaveChangesAsync();

        return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == product.Id) ?? product;
    }

    public async Task DeleteProductByIdAsync(Guid productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Models.Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }
}
