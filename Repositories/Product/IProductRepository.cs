namespace Inventra.Repositories.Product
{
    public interface IProductRepository
    {
        Task<List<Models.Product>> GetProductsAsync();
        Task<Models.Product?> GetProductByIdAsync(Guid productId);
        Task<Models.Product> CreateProductAsync(Models.Product product);
        Task<Models.Product> UpdateProductAsync(Models.Product product);
        Task DeleteProductByIdAsync(Guid productId);
        Task<List<Models.Product>> GetProductsByCategoryAsync(int categoryId);
    }
}