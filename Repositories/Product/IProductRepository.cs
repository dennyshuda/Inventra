namespace Inventra.Repositories.Product
{
    public interface IProductRepository
    {
        Task<List<Models.Product>> GetProductsAsync();
        Task<Models.Product?> GetProductByIdAsync(Guid id);
        Task<Models.Product> CreateProductAsync(Models.Product product);
        Task<Models.Product> UpdateProductAsync(Models.Product product);
    }
}