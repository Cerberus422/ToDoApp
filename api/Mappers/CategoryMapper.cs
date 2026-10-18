using api.Dtos;
using api.Dtos.Category;
using ToDoApp.api.Models;

namespace api.Mappers;

public static class CategoryMapper
{
    public static CategoryDto ToCategoryDto(this Category categoryModel)
    {
        return new CategoryDto
        {
            Id = categoryModel.Id,
            Name = categoryModel.Name
        };
    }
    
    public static Category ToCategoryFromCreate(this CreateCategoryDto categoryModel)
    {
        return new Category
        {
            Name = categoryModel.Name
        };
    }
}