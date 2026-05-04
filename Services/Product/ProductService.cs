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

    public async Task<ApiResponseDto<ProductDto>> CreateProductAsync(CreateProductDto createProductDto)
    {
        try
        {
            var product = _mapper.Map<Models.Product>(createProductDto);

            var createdProduct = await _productRepository.CreateProductAsync(product);
            var todoItemDto = _mapper.Map<ProductDto>(createdProduct);

            return ApiResponseDto<ProductDto>.SuccessResult(todoItemDto, "Product created successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<ProductDto>.ErrorResult($"Error creating product: {ex.Message}");
        }
    }


    public async Task<ApiResponseDto<ProductDto>> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto)
    {
        try
        {
            var existingTodoItem = await _productRepository.GetProductByIdAsync(id);

            if (existingTodoItem == null)
            {
                return ApiResponseDto<ProductDto>.ErrorResult("Product not found");
            }

            _mapper.Map(updateProductDto, existingTodoItem);

            var updatedProduct = await _productRepository.UpdateProductAsync(existingTodoItem);
            var productDto = _mapper.Map<ProductDto>(updatedProduct);

            return ApiResponseDto<ProductDto>.SuccessResult(productDto, "Todo item updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<ProductDto>.ErrorResult($"Error updating todo item: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<bool>> DeleteProductByIdAsync(Guid id)
    {
        try
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);

            if (existingProduct == null)
            {
                return ApiResponseDto<bool>.ErrorResult("Product not found");
            }

            await _productRepository.DeleteProductByIdAsync(id);

            return ApiResponseDto<bool>.SuccessResult(true, "Product deleted successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<bool>.ErrorResult($"Error deleting product: {ex.Message}");
        }
    }
}
