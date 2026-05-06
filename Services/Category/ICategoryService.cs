using Inventra.DTOs;
using Inventra.DTOs.Category;

namespace Inventra.Services.Category;

public interface ICategoryService
{
    Task<ApiResponseDto<List<CategoryDto>>> GetCategoriesAsync();
}