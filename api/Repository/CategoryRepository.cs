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

    public async Task<List<Category>> GetAllAsync()
    {
        var category = await _context.Categories.ToListAsync();

        return category;
    }

    public async Task<Category?> GetByIdAsync(int categoryId)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

        return category;
    }

    public async Task<Category> CreateAsync(Category categoryModel)
    {
        await _context.Categories.AddAsync(categoryModel);
        await _context.SaveChangesAsync();

        return categoryModel;
    }

    public async Task<Category?> PutAsync(int categoryId, Category categoryModel)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category == null)
            return null;

        category.Name = categoryModel.Name;

        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<Category?> DeleteAsync(int categoryId)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category == null)
            return null;

        _context.Remove(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public  Task<bool> ExistsAsync(int categoryId)
    {
        return  _context.Categories.AnyAsync(c => c.Id == categoryId);
    }
}