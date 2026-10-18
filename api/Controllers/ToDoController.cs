using api.Dtos;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
[Route("api/todo")]
[ApiController]
public class ToDoController : ControllerBase
{
    private readonly IToDoRepository _toDoRepo;
    private readonly ICategoryRepository _categoryRepo;

    public ToDoController(IToDoRepository toDoRepo, ICategoryRepository categoryRepo)
    {
        _toDoRepo = toDoRepo;
        _categoryRepo = categoryRepo;
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTodoDto todoDto) // Creating Task
    {

        if (todoDto.CategoryId.HasValue)
        {
            if (!await _categoryRepo.ExistsAsync(todoDto.CategoryId.Value))
            {
                return BadRequest("Category does not exist");
            }
        }
        
        var todoModel = todoDto.ToToDoFromCreateDto();

        await _toDoRepo.CreateAsync(todoModel);
        
        return CreatedAtAction(
            nameof(GetById),
            new { toDoId = todoModel.Id },
            todoModel.ToToDoDto()
        );
    }

    [HttpPatch("{todoId:int}")]
    public async Task<IActionResult> Patch([FromBody] PatchToDoDto todoDto, [FromRoute] int todoId) // Editing Task
    {
        if (todoDto.CategoryId.HasValue)
        {
            if (!await _categoryRepo.ExistsAsync(todoDto.CategoryId.Value))
            {
                return BadRequest("Category does not exist");
            }
        }
        
        var todo = await _toDoRepo.PatchAsync(todoId, todoDto);
        
        if (todo == null)
            return NotFound("ToDo not found");

        return Ok(todo.ToToDoDto());
    }

    [HttpDelete("{todoId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int todoId) // Deleting Task
    {
        var todo = await _toDoRepo.DeleteAsync(todoId);

        if (todo == null)
            return NotFound("ToDo not found");

        return NoContent();
    }
}