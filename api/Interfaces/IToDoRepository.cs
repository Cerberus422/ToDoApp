using api.Dtos;
using ToDoApp.api.Models;

namespace api.Interfaces;

public interface IToDoRepository
{
    Task<List<ToDo>> GetAllAsync(); // showing all Tasks
    Task<ToDo?> GetByIdAsync(int toDoId); // showing Task by Id
    Task<ToDo> CreateAsync (ToDo todoModel); // creating Task
    Task<ToDo?> PatchAsync (int todoId, PatchToDoDto patchDto); // patching Task
    Task<ToDo?> DeleteAsync(int productId);  // deleting Task
}