using Inventra.DTOs.Product;
using Inventra.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.Controllers;

// [Authorize]
[ApiController]
[Route("api/v1/product")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var response = await _productService.GetProductsAsync();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var response = await _productService.GetProductByIdAsync(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto product)
    {
        var response = await _productService.CreateProductAsync(product);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductUpdateDto product)
    {
        var response = await _productService.UpdateProductByIdAsync(id, product);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var response = await _productService.DeleteProductById(id);

        return Ok(response);
    }
}
