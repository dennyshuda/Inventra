using AutoMapper;
using Inventra.DTOs;
using Inventra.DTOs.Product;
using Inventra.Repositories.Product;

namespace Inventra.Services.Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }


    public async Task<ApiResponseDto<List<ProductDto>>> GetProductsAsync()
    {
        try
        {
            var products = await _productRepository.GetProductsAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            return ApiResponseDto<List<ProductDto>>.SuccessResult(productDtos);
        }
        catch (Exception ex)
        {
            return ApiResponseDto<List<ProductDto>>.ErrorResult($"Error retrieving products: {ex.Message}");
        }
    }


    public async Task<ApiResponseDto<ProductDto>> GetProductByIdAsync(Guid id)
    {
        try
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return ApiResponseDto<ProductDto>.ErrorResult("Product not found");
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return ApiResponseDto<ProductDto>.SuccessResult(productDto);
        }
        catch (Exception ex)
        {
            return ApiResponseDto<ProductDto>.ErrorResult($"Error retrieving product: {ex.Message}");
        }
    }
}
