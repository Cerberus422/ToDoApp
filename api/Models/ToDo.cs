using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.api.Models;
[Table(("ToDo"))]
public class ToDo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    
    public int?  CategoryId { get; set; }
    public Category? Category { get; set; }
}