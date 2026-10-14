using Microsoft.EntityFrameworkCore;
using ToDoApp.api.Models;

namespace api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions dbContextOptions)
    : base(dbContextOptions)
    {
        
    }
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<ToDo> ToDos { get; set; }
}