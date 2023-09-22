using AliensWebAPI.Dtos;
using AliensWebAPI.Interfaces;
using AliensWebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AliensWebAPI.Controllers;

[Route("api/[controller]")]
public class UfologistController : Controller
{
    private readonly IUfologistRepository _ufologistRepository;
    private readonly IMapper _mapper;

    public UfologistController(IUfologistRepository ufologistRepository, IMapper mapper)
    {
        _ufologistRepository = ufologistRepository;
        _mapper = mapper;
    }

    [HttpGet()]
    public IActionResult GetAllUfologists()
    {
        var ufologists = _ufologistRepository.GetUfologists();
        var ufologistDtos = _mapper.Map<List<UfologistDto>>(ufologists);
        return Ok(ufologistDtos);
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetUfologist(int id)
    {
        var ufologist = _ufologistRepository.GetUfologistReviews(id);
        var ufologistDto = _mapper.Map<UfologistDto>(ufologist);
        return Ok(ufologistDto);
    }
    
    [HttpGet("{ufologistId:int}/Reviews")]
    public IActionResult GetUfologistReviews(int ufologistId)
    {
        return Ok(_ufologistRepository.GetUfologistReviews(ufologistId));
    }

    [HttpPost()]
    public IActionResult CreateUfologist(UfologistCreateDto? ufologistCreateDto)
    {
        if (!ModelState.IsValid || ufologistCreateDto == null)
        {
            return BadRequest(ModelState);
        }

        var ufologst = _mapper.Map<Ufologist>(ufologistCreateDto);
        var result = _ufologistRepository.CreateUfologst(ufologst);
        if (!result)
        {
            ModelState.AddModelError("", "Saving error");
            return StatusCode(500, ModelState);
        }

        return Ok("Happy ufologist created!");
    }
}