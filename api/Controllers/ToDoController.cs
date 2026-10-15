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
    
    [HttpGet("{toDoId:int}")]
    public async Task<IActionResult> GetById([FromRoute] int toDoId) // Showing Task by Id
    {
        var todo = await _toDoRepo.GetByIdAsync(toDoId);

        if (todo == null)
            return NotFound("ToDo not found");
        
        var tasksModel = todo.ToToDoDto();

        return Ok(tasksModel);
    }

}