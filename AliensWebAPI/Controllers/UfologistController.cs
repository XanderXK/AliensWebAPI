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
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    private const string CreatedMessage = "Happy ufologist created!";
    private const string SaveErrorMessage = "Something went wrong";
    private const string NotFoundMessage = "Ufologist not found";


    public UfologistController(IUfologistRepository ufologistRepository, IReviewRepository reviewRepository, IMapper mapper)
    {
        _ufologistRepository = ufologistRepository;
        _reviewRepository = reviewRepository;
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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var ufologist = _ufologistRepository.GetUfologist(id);
        var ufologistDto = _mapper.Map<UfologistDto>(ufologist);
        return Ok(ufologistDto);
    }

    [HttpGet("{ufologistId:int}/Reviews")]
    public IActionResult GetUfologistReviews(int ufologistId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(_ufologistRepository.GetUfologistReviews(ufologistId));
    }

    [HttpPost("{name}")]
    public IActionResult CreateUfologist(string name)
    {
        if (!ModelState.IsValid || string.IsNullOrEmpty(name))
        {
            return BadRequest(ModelState);
        }

        var ufologst = new Ufologist { Name = name };
        var result = _ufologistRepository.CreateUfologst(ufologst);
        if (!result)
        {
            ModelState.AddModelError("", SaveErrorMessage);
            return StatusCode(500, ModelState);
        }

        return Ok(CreatedMessage);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteUfologistWithPosts(int id)
    {
        var ufologist = _ufologistRepository.GetUfologist(id);
        if (ufologist == null)
        {
            return NotFound(NotFoundMessage);
        }

        var reviews = _ufologistRepository.GetUfologistReviews(id);
        _reviewRepository.DeleteReviews(reviews);

        var result = _ufologistRepository.DeleteUfologst(ufologist);
        if (!result)
        {
            ModelState.AddModelError("", SaveErrorMessage);
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}