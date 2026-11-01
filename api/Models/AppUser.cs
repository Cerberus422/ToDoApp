using Microsoft.AspNetCore.Identity;

namespace ToDoApp.api.Models;

public class AppUser : IdentityUser
{
    public List<ToDo> Todos { get; set; } = new List<ToDo>();

    public List<Category> Categories { get; set; } = new List<Category>();
}