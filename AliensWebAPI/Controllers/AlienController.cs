using AliensWebAPI.Dtos;
using AliensWebAPI.Interfaces;
using AliensWebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AliensWebAPI.Controllers;

[Route("api/[controller]")]
public class AlienController : Controller
{
    private readonly IAlienRepository _alienRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public AlienController(IAlienRepository alienRepository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _alienRepository = alienRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAliens()
    {
        var aliens = _alienRepository.GetAliens();
        return Ok(_mapper.Map<List<AlienDto>>(aliens));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAlien(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var alien = _alienRepository.GetAlien(id);
        return Ok(_mapper.Map<AlienDto>(alien));
    }

    [HttpGet("{id:int}/Rating")]
    public IActionResult GetAlienRating(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(_alienRepository.GetAlienRating(id));
    }

    [HttpPost]
    public IActionResult CreateAlien(AlienDto? alienDto)
    {
        if (!ModelState.IsValid || alienDto == null)
        {
            return BadRequest(ModelState);
        }

        var category = _categoryRepository.GetCategory(alienDto.CategoryName);
        if (category == null)
        {
            category = new Category
            {
                Name = alienDto.CategoryName
            };

            _categoryRepository.CreateCategory(category);
        }

        var alien = _mapper.Map<Alien>(alienDto);
        alien.Category = category;

        var result = _alienRepository.CreateAlien(alien);
        if (!result)
        {
            ModelState.AddModelError("", "Saving error");
            return StatusCode(500, ModelState);
        }

        return Ok("Happy ufologist created");
    }

    [HttpPut]
    public IActionResult UpdateAlien(AlienDto alienDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var alien = _alienRepository.GetAlien(alienDto.Id);
        if (alien == null)
        {
            return NotFound("Alien with this id is not found");
        }

        alien = _mapper.Map<Alien>(alienDto);
        var result = _alienRepository.UpdateAlien(alien);
        if (!result)
        {
            ModelState.AddModelError("", "Saving error");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}