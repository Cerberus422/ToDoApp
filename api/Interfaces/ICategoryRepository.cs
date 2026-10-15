namespace api.Interfaces;

public interface ICategoryRepository
{
    Task<bool> ExistsAsync(int categoryId); // checking if category exists
}