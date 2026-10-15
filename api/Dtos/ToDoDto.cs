namespace api.Dtos;

public class ToDoDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    
    public int? CategoryId { get; set; }
}