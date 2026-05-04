using FluentValidation;
using FluentValidation.Results;
using Inventra.DTOs;
using Inventra.DTOs.Product;
using Inventra.Services;
using Inventra.Services.Product;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.Controllers;

// [Authorize]
[ApiController]
[Route("api/v1/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IValidator<CreateProductDto> _createProductValidator;
    private readonly IValidator<UpdateProductDto> _updateProductValidator;

    public ProductController(IProductService productService, IValidator<CreateProductDto> createProductValidator, IValidator<UpdateProductDto> updateProductValidator)
    {
        _productService = productService;
        _createProductValidator = createProductValidator;
        _updateProductValidator = updateProductValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var result = await _productService.GetProductsAsync();

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var result = await _productService.GetProductByIdAsync(id);

        if (!result.Success)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        var validationResult = await _createProductValidator.ValidateAsync(createProductDto);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            var errorResponse = ApiResponseDto<ProductDto>.ErrorResult("Validation failed", errors);
            return BadRequest(errorResponse);
        }

        var result = await _productService.CreateProductAsync(createProductDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetProductById), new { id = result.Data?.Id }, result);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto updateProductDto)
    {
        // var response = await _productService.UpdateProductAsync(id, updateProductDto);

        var validationResult = await _updateProductValidator.ValidateAsync(updateProductDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            var errorResponse = ApiResponseDto<ProductDto>.ErrorResult("Validation failed", errors);
            return BadRequest(errorResponse);
        }


        var result = await _productService.UpdateProductAsync(id, updateProductDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    // [HttpPut("{id}")]
    //     public async Task<IActionResult> Update(int id, [FromBody] UpdateTodoItemDto updateDto)
    //     {
    //         var userId = GetCurrentUserId();
    //         if (string.IsNullOrEmpty(userId))
    //             return Unauthorized();

    //         var validationResult = await _updateValidator.ValidateAsync(updateDto);
    //         if (!validationResult.IsValid)
    //         {
    //             var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
    //             var errorResponse = ApiResponseDto<TodoItemDto>.ErrorResult("Validation failed", errors);
    //             return BadRequest(errorResponse);
    //         }

    //         var result = await _todoItemService.UpdateAsync(id, updateDto, userId);

    //         if (!result.Success)
    //         {
    //             return BadRequest(result);
    //         }

    //         return Ok(result);
    //     }



    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteProduct(Guid id)
    // {
    //     var response = await _productService.DeleteProductById(id);

    //     return Ok(response);
    // }
}
