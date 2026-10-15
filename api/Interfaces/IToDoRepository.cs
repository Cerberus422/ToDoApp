using ToDoApp.api.Models;

namespace api.Interfaces;

public interface IToDoRepository
{
    Task<List<ToDo>> GetAllAsync(); // showing all Tasks
    Task<ToDo?> GetByIdAsync(int toDoId); // Showing Task by Id
}