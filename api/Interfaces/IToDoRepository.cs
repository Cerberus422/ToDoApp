using api.Dtos;
using ToDoApp.api.Models;

namespace api.Interfaces;

public interface IToDoRepository
{
    Task<List<ToDo>> GetAllAsync(string userId); // showing all Tasks
    Task<ToDo?> GetByIdAsync(int toDoId, string userId); // showing Task by Id
    Task<ToDo> CreateAsync (ToDo todoModel, string userId); // creating Task
    Task<ToDo?> PatchAsync (int todoId, PatchToDoDto patchDto, string userId); // patching Task
    Task<ToDo?> DeleteAsync(int productId, string userId);  // deleting Task
}