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
    
}