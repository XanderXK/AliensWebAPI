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
    private readonly ISolarSystemRepository _solarSystemRepository;
    private readonly IMapper _mapper;

    private const string AlienNotFoundMessage = "Alien not found";
    private const string SolarSystemNotFoundMessage = "Solar System not found";
    private const string AlreadyExistsMessage = "Alien already exists";
    private const string SaveErrorMessage = "Something went wrong";
    private const string CreatedMessage = "Alien created";

    public AlienController(IAlienRepository alienRepository, ICategoryRepository categoryRepository, ISolarSystemRepository solarSystemRepository, IMapper mapper)
    {
        _alienRepository = alienRepository;
        _categoryRepository = categoryRepository;
        _solarSystemRepository = solarSystemRepository;
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
        if (alien == null)
        {
            return NotFound(AlienNotFoundMessage);
        }

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
    public IActionResult CreateAlien(AlienCreateDto alienCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var exists = _alienRepository.GetAliens()
            .Any(a => string.Equals(a.Name, alienCreateDto.Name, StringComparison.CurrentCultureIgnoreCase));
        
        if (exists)
        {
            ModelState.AddModelError("", AlreadyExistsMessage);
            return StatusCode(422, ModelState);
        }

        var category = _categoryRepository.GetCategory(alienCreateDto.CategoryName);
        if (category == null)
        {
            category = new Category { Name = alienCreateDto.CategoryName };
            _categoryRepository.CreateCategory(category);
        }

        var solarSystems = new List<SolarSystem>();
        if (alienCreateDto.SolarSystemIds != null)
        {
            foreach (var solarSystemId in alienCreateDto.SolarSystemIds)
            {
                var currentSystem = _solarSystemRepository.GetSolarSystem(solarSystemId);
                if (currentSystem == null)
                {
                    return NotFound(SolarSystemNotFoundMessage);
                }

                solarSystems.Add(currentSystem);
            }
        }

        var alien = _mapper.Map<Alien>(alienCreateDto);
        alien.Category = category;
        var result = _alienRepository.CreateAlien(alien, solarSystems);
        if (!result)
        {
            ModelState.AddModelError("", SaveErrorMessage);
            return StatusCode(500, ModelState);
        }

        return Ok(CreatedMessage);
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
            return NotFound(AlienNotFoundMessage);
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