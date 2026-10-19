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
            Difficulty = toDoModel.Difficulty,
            DueDate = toDoModel.DueDate,
            IsCompleted = toDoModel.IsCompleted,

            CategoryId = toDoModel.CategoryId
        };
    }
    
    public static ToDo ToToDoFromCreateDto(this CreateTodoDto toDoModel)
    {
        return new ToDo
        {
            Title = toDoModel.Title,
            Difficulty = toDoModel.Difficulty,
            DueDate = toDoModel.DueDate,
            IsCompleted = false,
            
            CategoryId = toDoModel.CategoryId
        };
    }
}