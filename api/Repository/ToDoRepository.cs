using api.Data;
using api.Dtos;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using ToDoApp.api.Models;

namespace api.Repository;

public class ToDoRepository : IToDoRepository
{
    private readonly ApplicationDbContext _context;

    public ToDoRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<ToDo>> GetAllAsync(string userId)
    {
        var todos = await _context.ToDos.Where(t => t.AppUserId == userId).ToListAsync();

        return todos;
    }

    public async Task<ToDo?> GetByIdAsync(int toDoId, string userId)
    {
        var todo = await _context.ToDos.Where(t => t.AppUserId == userId).FirstOrDefaultAsync(t => t.Id == toDoId);

        return todo;
    }

    public async Task<ToDo> CreateAsync(ToDo todoModel, string userId)
    {
        todoModel.AppUserId = userId;
        
        await _context.ToDos.AddAsync(todoModel);
        await _context.SaveChangesAsync();

        return todoModel;
    }

    public async Task<ToDo?> PatchAsync(int todoId, PatchToDoDto patchDto, string userId)
    {
        var todo = await _context.ToDos
            .FirstOrDefaultAsync(t => t.Id == todoId && t.AppUserId == userId);
        
        if (todo == null)
            return null;
        
        if (patchDto.Title != null)
            todo.Title = patchDto.Title;

        if (patchDto.Difficulty != null)
            todo.Difficulty = patchDto.Difficulty;
        
        if (patchDto.DueDate.HasValue)
            todo.DueDate = patchDto.DueDate.Value;
        
        if (patchDto.IsCompleted.HasValue)
            todo.IsCompleted = patchDto.IsCompleted.Value;
             
        if (patchDto.CategoryId.HasValue)   
            todo.CategoryId = patchDto.CategoryId.Value;
        
        await _context.SaveChangesAsync();

        return todo;
    }

    public async Task<ToDo?> DeleteAsync(int productId, string userId)
    {
        var todo = await _context.ToDos
            .FirstOrDefaultAsync(t => t.Id == productId && t.AppUserId == userId);

        if (todo == null)
            return null;

        _context.ToDos.Remove(todo);
        await _context.SaveChangesAsync();

        return todo;
    }
}