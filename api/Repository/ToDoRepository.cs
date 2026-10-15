using api.Data;
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
    
    public async Task<List<ToDo>> GetAllAsync()
    {
        var todos = await _context.ToDos.ToListAsync();

        return todos;
    }
}