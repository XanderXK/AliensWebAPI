using AliensWebAPI.Dtos;
using AliensWebAPI.Interfaces;
using AliensWebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AliensWebAPI.Controllers;

[Route("api/[controller]")]
public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    private const string NotFoundMessage = "Category not found";
    private const string AlreadyExistsMessage = "Category already exists";
    private const string SaveErrorMessage = "Something went wrong";
    private const string CreatedMessage = "Category Created";
    private const string DeleteCrowdedMessage = "Category is not empty";
    
    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _categoryRepository.GetCategories();
        var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
        return Ok(categoryDtos);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetCategory(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = _categoryRepository.GetCategory(id);
        if (category == null)
        {
            return NotFound(NotFoundMessage);
        }

        return Ok(_mapper.Map<CategoryDto>(category));
    }

    [HttpGet("{categoryId:int}/Aliens")]
    public IActionResult GetAliensByCategory(int categoryId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var aliens = _categoryRepository.GetAliensByCategory(categoryId);
        var alienDtos = _mapper.Map<List<AlienDto>>(aliens);
        return Ok(alienDtos);
    }

    [HttpPost]
    public IActionResult CreateCategory(CategoryCreateDto categoryCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var exists = _categoryRepository.GetCategories()
            .Any(c => string.Equals(c.Name, categoryCreateDto.Name, StringComparison.CurrentCultureIgnoreCase));

        if (exists)
        {
            ModelState.AddModelError("", AlreadyExistsMessage);
            return StatusCode(422, ModelState);
        }

        var category = _mapper.Map<Category>(categoryCreateDto);
        var result = _categoryRepository.CreateCategory(category);
        if (!result)
        {
            ModelState.AddModelError("", SaveErrorMessage);
            return StatusCode(500, ModelState);
        }

        return Ok(CreatedMessage);
    }

    [HttpPut]
    public IActionResult UpdateCategory(CategoryDto categoryDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = _categoryRepository.GetCategory(categoryDto.Id);
        if (category == null)
        {
            return NotFound(NotFoundMessage);
        }

        category.Name = categoryDto.Name;
        category.Description = categoryDto.Description;

        var result = _categoryRepository.UpdateCategory(category);
        if (!result)
        {
            ModelState.AddModelError("", SaveErrorMessage);
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }

    [HttpDelete]
    public IActionResult DeleteCategory(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = _categoryRepository.GetCategory(id);
        if (category == null)
        {
            return NotFound(NotFoundMessage);
        }

        var aliens = _categoryRepository.GetAliensByCategory(id);
        if (aliens.Count != 0)
        {
            ModelState.AddModelError("", DeleteCrowdedMessage);
            return StatusCode(422, ModelState);
        }

        var result = _categoryRepository.DeleteCategory(category);
        if (!result)
        {
            ModelState.AddModelError("", SaveErrorMessage);
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}