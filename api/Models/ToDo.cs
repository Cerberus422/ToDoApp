using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;

namespace ToDoApp.api.Models;
[Table(("ToDo"))]
public class ToDo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Difficulty? Difficulty { get; set; }
    public DateOnly? DueDate { get; set; }
    public bool IsCompleted { get; set; }
    
    public int?  CategoryId { get; set; }
    public Category? Category { get; set; }
}