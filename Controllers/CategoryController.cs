using Inventra.Services.Category;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.Controllers;

[ApiController]
[Route("api/v1/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _categoryService.GetCategoriesAsync();

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
