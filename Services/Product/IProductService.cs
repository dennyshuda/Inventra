using Inventra.DTOs;
using Inventra.DTOs.Product;

namespace Inventra.Services.Product;

public interface IProductService
{
    Task<ApiResponseDto<List<ProductDto>>> GetProductsAsync();
    Task<ApiResponseDto<ProductDto>> GetProductByIdAsync(Guid id);
    Task<ApiResponseDto<ProductDto>> CreateProductAsync(CreateProductDto createProductDto);
    Task<ApiResponseDto<ProductDto>> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto);
}
