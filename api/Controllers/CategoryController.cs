using api.Dtos.Category;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepo;
    
    public CategoryController(ICategoryRepository categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() // Showing all categories
    {
        var categories = await _categoryRepo.GetAllAsync();

        var categoryModels = categories.Select(c => c.ToCategoryDto()).ToList();

        return Ok(categoryModels);
    }


    [HttpGet("{categoryId:int}")]
    public async Task<IActionResult> GetById([FromRoute] int categoryId) // Showing Category by Id
    {
        var category = await _categoryRepo.GetByIdAsync(categoryId);

        if (category == null)
            return NotFound("Category not found");

        var categoryModel = category.ToCategoryDto();

        return Ok(categoryModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto categoryDto) // Creating Category
    {

        var categoryModel = categoryDto.ToCategoryFromCreate();

        await _categoryRepo.CreateAsync(categoryModel);

        return CreatedAtAction(
            nameof(GetById),
            new { categoryId = categoryModel.Id },
            categoryModel.ToCategoryDto()
        );
    }
    
    
    
    
    
    
    
    
    
}