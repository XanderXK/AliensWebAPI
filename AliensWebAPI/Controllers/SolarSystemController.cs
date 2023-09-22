using AliensWebAPI.Dtos;
using AliensWebAPI.Interfaces;
using AliensWebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AliensWebAPI.Controllers;

[Route("api/[controller]")]
public class SolarSystemController : Controller
{
    private readonly ISolarSystemRepository _solarSystemRepository;
    private readonly IMapper _mapper;

    private const string NotFoundMessage = "Solar system not found";
    private const string AlreadyExistsMessage = "Solar system already exists";
    private const string SaveErrorMessage = "Something went wrong";
    private const string CreatedMessage = "Solar system created";

    public SolarSystemController(ISolarSystemRepository solarSystemRepository, IMapper mapper)
    {
        _solarSystemRepository = solarSystemRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetSolarSystems()
    {
        var solarSystems = _solarSystemRepository.GetSolarSystems();
        var solarSystemDtos = _mapper.Map<List<SolarSystemDto>>(solarSystems);
        return Ok(solarSystemDtos);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetSolarSystem(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var solarSystem = _solarSystemRepository.GetSolarSystem(id);
        if (solarSystem == null)
        {
            return NotFound(NotFoundMessage);
        }

        var solarSystemDto = _mapper.Map<SolarSystemDto>(solarSystem);
        return Ok(solarSystemDto);
    }

    [HttpGet("{solarSystemId:int}/Aliens")]
    public IActionResult GetAliensInSystem(int solarSystemId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var aliens = _solarSystemRepository.GetAliensInSystem(solarSystemId);
        var alienDtos = _mapper.Map<List<AlienDto>>(aliens);
        return Ok(alienDtos);
    }

    [HttpPost]
    public IActionResult CreateSolarSystem(SolarSystemDto? solarSystemDto)
    {
        if (!ModelState.IsValid || solarSystemDto == null)
        {
            return BadRequest(ModelState);
        }

        var exists = _solarSystemRepository.GetSolarSystems().Any(s => string.Equals(s.Name, solarSystemDto.Name, StringComparison.CurrentCultureIgnoreCase));
        if (exists)
        {
            ModelState.AddModelError("", AlreadyExistsMessage);
            return StatusCode(422, ModelState);
        }

        var solarSystem = _mapper.Map<SolarSystem>(solarSystemDto);
        var result = _solarSystemRepository.CreateSolarSystem(solarSystem);
        if (!result)
        {
            ModelState.AddModelError("", SaveErrorMessage);
            return StatusCode(500, ModelState);
        }

        return Ok(CreatedMessage);
    }

    [HttpPut]
    public IActionResult UpdateSolarSystem(SolarSystemDto solarSystemDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var solarSystem = _solarSystemRepository.GetSolarSystem(solarSystemDto.Id);
        if (solarSystem == null)
        {
            return NotFound(NotFoundMessage);
        }
        
        solarSystem.Name = solarSystemDto.Name;
        solarSystem.Address = solarSystemDto.Address;

        var result = _solarSystemRepository.UpdateSolarSystem(solarSystem);
        if (!result)
        {
            ModelState.AddModelError("", SaveErrorMessage);
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}