using AutoMapper;
using Inventra.DTOs;
using Inventra.DTOs.Category;
using Inventra.Repositories.Category;

namespace Inventra.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponseDto<List<CategoryDto>>> GetCategoriesAsync()
    {
        try
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return ApiResponseDto<List<CategoryDto>>.SuccessResult(categoryDtos);
        }
        catch (Exception ex)
        {
            return ApiResponseDto<List<CategoryDto>>.ErrorResult($"Error retrieving categories: {ex.Message}");
        }
    }
}