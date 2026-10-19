using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Dtos;

public class CreateTodoDto
{
    [Required]
    [MaxLength(100,ErrorMessage = "Title cannot be over 100 characters")]
    public string Title { get; set; } = string.Empty;
    [EnumDataType(typeof(Difficulty),ErrorMessage = "Difficulty must be Easy, Medium, or Hard.")]
    public Difficulty? Difficulty { get; set; }
    public DateOnly? DueDate { get; set; }
    public int? CategoryId { get; set; }
}