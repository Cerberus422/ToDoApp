using ToDoApp.api.Models;

namespace api.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(); // showing all Categories
    Task<bool> ExistsAsync(int categoryId); // checking if Category exists
}