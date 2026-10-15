using ToDoApp.api.Models;

namespace api.Interfaces;

public interface IToDoRepository
{
    Task<List<ToDo>> GetAllAsync(); // showing all Tasks
}