using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Category;

public class CreateCategoryDto
{
    [Required]
    [MaxLength(30,ErrorMessage = "Name cannot be over 30 characters")]
    public string Name { get; set; } = string.Empty;
}