namespace Inventra.Repositories.Category;

public interface ICategoryRepository
{
    Task<List<Models.Category>> GetCategoriesAsync();
    Task<Models.Category?> GetCategoryByIdAsync(int categoryId);
    Task<Models.Category> CreateCategoryAsync(Models.Category category);
    Task<Models.Category> UpdateCategoryAsync(Models.Category category);
    Task DeleteCategoryByIdAsync(int categoryId);
}
