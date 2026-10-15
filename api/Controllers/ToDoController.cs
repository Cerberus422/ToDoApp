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
        
        var todoModel = todoDto.ToToDoFromCreate();

        await _toDoRepo.CreateAsync(todoModel);
        
        return CreatedAtAction(
            nameof(GetById),
            new { toDoId = todoModel.Id },
            todoModel.ToToDoDto()
        );
    }
    
}