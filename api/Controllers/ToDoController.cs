using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
[Route("api/todo")]
[ApiController]
public class ToDoController : ControllerBase
{
    private readonly IToDoRepository _toDoRepo;

    public ToDoController(IToDoRepository toDoRepo)
    {
        _toDoRepo = toDoRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() // Showing all Tasks
    {
        var todos = await _toDoRepo.GetAllAsync();
        
        var tasksModel = todos.Select(t => t.ToToDoDto()).ToList();

        return Ok(tasksModel);
    }
}