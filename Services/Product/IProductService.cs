using Inventra.DTOs;
using Inventra.DTOs.Product;

namespace Inventra.Services.Product;

public interface IProductService
{
    Task<ApiResponseDto<List<ProductDto>>> GetProductsAsync();
    Task<ApiResponseDto<ProductDto>> GetProductByIdAsync(Guid productId);
    Task<ApiResponseDto<ProductDto>> CreateProductAsync(CreateProductDto createProductDto);
    Task<ApiResponseDto<ProductDto>> UpdateProductAsync(Guid productId, UpdateProductDto updateProductDto);
    Task<ApiResponseDto<bool>> DeleteProductByIdAsync(Guid productId);
    Task<ApiResponseDto<List<ProductDto>>> GetProductsByCategoryAsync(int categoryId);
}
