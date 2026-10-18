using api.Dtos;
using ToDoApp.api.Models;

namespace api.Mappers;

public static class ToDoMapper
{
    public static ToDoDto ToToDoDto(this ToDo toDoModel)
    {
        return new ToDoDto
        {
            Id = toDoModel.Id,
            Title = toDoModel.Title,
            IsCompleted = toDoModel.IsCompleted,

            CategoryId = toDoModel.CategoryId
        };
    }
    
    public static ToDo ToToDoFromCreateDto(this CreateTodoDto toDoModel)
    {
        return new ToDo
        {
            Title = toDoModel.Title,
            IsCompleted = false,
            
            CategoryId = toDoModel.CategoryId
        };
    }
}