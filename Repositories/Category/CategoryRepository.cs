
namespace Inventra.Repositories.Category;

public class CategoryRepository : ICategoryRepository
{
    public Task<List<Models.Category>> GetCategoriesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Models.Category?> GetCategoryByIdAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Category> CreateCategoryAsync(Models.Category category)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Category> UpdateCategoryAsync(Models.Category category)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCategoryByIdAsync(int categoryId)
    {
        throw new NotImplementedException();
    }



}