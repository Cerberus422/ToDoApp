using api.Data;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using ToDoApp.api.Models;

namespace api.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllAsync(string userId)
    {
        var categories  = await _context.Categories
            .Where(c => c.AppUserId == userId)
            .ToListAsync();

        return categories ;
    }

    public async Task<Category?> GetByIdAsync(int categoryId, string userId)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId && c.AppUserId == userId);

        return category;
    }

    public async Task<Category> CreateAsync(Category categoryModel, string userId)
    {
        categoryModel.AppUserId = userId;
        
        await _context.Categories.AddAsync(categoryModel);
        await _context.SaveChangesAsync();

        return categoryModel;
    }

    public async Task<Category?> PutAsync(int categoryId, Category categoryModel, string userId)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId &&  c.AppUserId == userId);

        if (category == null)
            return null;

        category.Name = categoryModel.Name;

        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<Category?> DeleteAsync(int categoryId, string userId)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId &&  c.AppUserId == userId);

        if (category == null)
            return null;

        _context.Remove(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public Task<bool> ExistsAsync(int categoryId, string userId)
    {
        return _context.Categories
            .AnyAsync(c => c.Id == categoryId && c.AppUserId == userId);
    }
}