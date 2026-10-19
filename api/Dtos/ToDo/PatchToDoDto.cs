using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Dtos;

public class PatchToDoDto
{
    [MaxLength(100,ErrorMessage = "Title cannot be over 100 characters")]
    public string? Title { get; set; } 
    [EnumDataType(typeof(Difficulty),ErrorMessage = "Difficulty must be Easy, Medium, or Hard.")]
    public Difficulty? Difficulty { get; set; }
    public DateOnly? DueDate { get; set; }
    public bool? IsCompleted { get; set; }
    public int? CategoryId { get; set; }
}