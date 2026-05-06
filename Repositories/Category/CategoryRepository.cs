
using Inventra.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Repositories.Category;

public class CategoryRepository : ICategoryRepository
{

    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Models.Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
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