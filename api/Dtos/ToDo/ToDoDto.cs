using api.Enums;

namespace api.Dtos;

public class ToDoDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Difficulty? Difficulty { get; set; }
    public DateOnly? DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public int? CategoryId { get; set; }
}