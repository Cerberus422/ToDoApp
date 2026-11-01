using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.api.Models;
[Table(("Category"))]
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public List<ToDo> Todos { get; set; } = new List<ToDo>();
}