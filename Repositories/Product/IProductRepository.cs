using Inventra.DTOs.Product;

namespace Inventra.Repositories.Product;

public interface IProductRepository
{
    Task<List<ProductDto>> GetProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(Guid id);
}
