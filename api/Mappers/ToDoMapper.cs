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
}