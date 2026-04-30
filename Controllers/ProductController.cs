using Inventra.Data;
using Inventra.DTOs.Product;
using Inventra.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Controllers;

[ApiController]
[Route("api/v1/product")]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(ProductCreateDto product)
    {
        var newProduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = product.Name,
            Sku = product.Sku,
            PurchasePrice = product.PurchasePrice,
            SellingPrice = product.SellingPrice,
            Stock = product.Stock
        };

        _context.Products.Add(newProduct);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
    }
}
