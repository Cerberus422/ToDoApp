using api.Dtos;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<IActionResult> GetAll() // Showing all Tasks
    {
        var userId = User.GetUserId();
        
        var todos = await _toDoRepo.GetAllAsync(userId);
        
        var tasksModel = todos.Select(t => t.ToToDoDto()).ToList();

        return Ok(tasksModel);
    }
    
    [HttpGet("{toDoId:int}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] int toDoId) // Showing Task by Id
    {
        var userId = User.GetUserId();
        
        var todo = await _toDoRepo.GetByIdAsync(toDoId, userId);

        if (todo == null)
            return NotFound("ToDo not found");
        
        var tasksModel = todo.ToToDoDto();

        return Ok(tasksModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateTodoDto todoDto) // Creating Task
    {
        var userId = User.GetUserId();
        
        if (todoDto.CategoryId.HasValue)
        {
            if (!await _categoryRepo.ExistsAsync(todoDto.CategoryId.Value, userId))
            {
                return BadRequest("Category does not exist");
            }
        }
        
        var todoModel = todoDto.ToToDoFromCreateDto();

        await _toDoRepo.CreateAsync(todoModel, userId);
        
        return CreatedAtAction(
            nameof(GetById),
            new { toDoId = todoModel.Id },
            todoModel.ToToDoDto()
        );
    }

    [HttpPatch("{todoId:int}")]
    [Authorize]
    public async Task<IActionResult> Patch([FromBody] PatchToDoDto todoDto, [FromRoute] int todoId) // Editing Task
    {
        var userId = User.GetUserId();
        
        if (todoDto.CategoryId.HasValue)
        {
            if (!await _categoryRepo.ExistsAsync(todoDto.CategoryId.Value, userId))
            {
                return BadRequest("Category does not exist");
            }
        }
        
        var todo = await _toDoRepo.PatchAsync(todoId, todoDto, userId);
        
        if (todo == null)
            return NotFound("ToDo not found");

        return Ok(todo.ToToDoDto());
    }

    [HttpDelete("{todoId:int}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int todoId) // Deleting Task
    {
        var userId = User.GetUserId();
        
        var todo = await _toDoRepo.DeleteAsync(todoId, userId);

        if (todo == null)
            return NotFound("ToDo not found");

        return NoContent();
    }
}