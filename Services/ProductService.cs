using Inventra.Data;
using Inventra.DTOs;
using Inventra.DTOs.Product;
using Inventra.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Services;

public class ProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<IEnumerable<Product>>> GetProductsAsync()
    {
        var products = await _context.Products.ToListAsync();
        return new BaseResponse<IEnumerable<Product>>
        {
            Success = true,
            Data = products
        };
    }

    public async Task<BaseResponse<Product>> GetProductByIdAsync(Guid id)
    {
        Product? product = await _context.Products.FindAsync(id);

        return new BaseResponse<Product>
        {
            Success = true,
            Data = product
        };
    }

    public async Task<BaseResponse<Product>> DeleteProductById(Guid id)
    {
        Product? product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return new BaseResponse<Product>
            {
                Success = false,
                Data = product
            };
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return new BaseResponse<Product>
        {
            Success = true,
            Data = product
        };
    }

    public async Task<BaseResponse<Product>> CreateProductAsync(ProductCreateDto dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Sku = dto.Sku,
            PurchasePrice = dto.PurchasePrice,
            SellingPrice = dto.SellingPrice,
            Stock = dto.Stock
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return new BaseResponse<Product>
        {
            Success = true,
            Message = "Produk berhasil ditambahkan",
            Data = product
        };
    }

    public async Task<BaseResponse<Product>> UpdateProductByIdAsync(Guid id, ProductUpdateDto dto)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return new BaseResponse<Product>
            {
                Success = false,
                Data = null
            };
        }

        if (!string.IsNullOrEmpty(dto.Name))
            product.Name = dto.Name;

        if (!string.IsNullOrEmpty(dto.Sku))
            product.Sku = dto.Sku;

        if (dto.PurchasePrice.HasValue)
            product.PurchasePrice = dto.PurchasePrice.Value;

        if (dto.SellingPrice.HasValue)
            product.SellingPrice = dto.SellingPrice.Value;

        if (dto.Stock.HasValue)
            product.Stock = dto.Stock.Value;

        await _context.SaveChangesAsync();

        return new BaseResponse<Product>
        {
            Success = true,
            Message = "Produk berhasil diupdate",
            Data = product
        };
    }

}