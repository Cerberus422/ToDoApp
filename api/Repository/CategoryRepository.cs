using api.Data;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }
        
    public  Task<bool> ExistsAsync(int categoryId)
    {
        return  _context.Categories.AnyAsync(c => c.Id == categoryId);
    }
}