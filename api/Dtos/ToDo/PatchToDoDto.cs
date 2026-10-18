using System.ComponentModel.DataAnnotations;

namespace api.Dtos;

public class PatchToDoDto
{
    [MaxLength(100,ErrorMessage = "Title cannot be over 100 characters")]
    public string? Title { get; set; } 

    public int? CategoryId { get; set; }
    
    public bool? IsCompleted { get; set; }
}