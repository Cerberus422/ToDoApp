using System.ComponentModel.DataAnnotations;

namespace api.Dtos;

public class CreateTodoDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public int? CategoryId { get; set; }
}