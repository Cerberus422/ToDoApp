using ToDoApp.api.Models;

namespace api.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(string userId); // showing all Categories
    Task<Category?> GetByIdAsync(int categoryId, string userId); // showing Category by Id
    Task<Category> CreateAsync(Category categoryModel, string userId); // creating Category
    Task<Category?> PutAsync(int categoryId, Category categoryModel, string userId); // updating Category
    Task<Category?> DeleteAsync(int categoryId, string userId);  // deleting Category
    Task<bool> ExistsAsync(int categoryId, string userId); // checking if Category exists
}