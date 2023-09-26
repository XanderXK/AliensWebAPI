using AliensWebAPI.Dtos;
using AliensWebAPI.Interfaces;
using AliensWebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AliensWebAPI.Controllers;

[Route("api/[controller]")]
public class ReviewController : Controller
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IAlienRepository _alienRepository;
    private readonly IUfologistRepository _ufologistRepository;
    private readonly IMapper _mapper;

    private const string NotFoundMessage = "Review not found";
    private const string SaveErrorMessage = "Something went wrong";
    private const string CreatedMessage = "Review Created";
    private const string NoAlienMessage = "No alien with this id";
    private const string NoUfologistMessage = "No ufologist with this id";

    public ReviewController(IReviewRepository reviewRepository, IAlienRepository alienRepository, IUfologistRepository ufologistRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _alienRepository = alienRepository;
        _ufologistRepository = ufologistRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetReviews()
    {
        var reviews = _reviewRepository.GetReviews();
        var reviewDtos = _mapper.Map<List<ReviewDto>>(reviews);
        return Ok(reviewDtos);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetReview(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var review = _reviewRepository.GetReview(id);
        if (review == null)
        {
            return NotFound(NotFoundMessage);
        }

        var reviewDto = _mapper.Map<ReviewDto>(review);
        return Ok(reviewDto);
    }

    [HttpGet("Alien/{alienId:int}")]
    public IActionResult GetAlienReviews(int alienId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (_alienRepository.GetAlien(alienId) == null)
        {
            return NotFound(NoAlienMessage);
        }

        var reviews = _reviewRepository.GetAlienReviews(alienId);
        var reviewDtos = _mapper.Map<List<ReviewDto>>(reviews);
        return Ok(reviewDtos);
    }

    [HttpPost]
    public IActionResult CreateReview(ReviewCreateDto reviewCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var alien = _alienRepository.GetAlien(reviewCreateDto.AlienId);
        if (alien == null)
        {
            return NotFound(NoAlienMessage);
        }

        var ufologist = _ufologistRepository.GetUfologist(reviewCreateDto.UfologistId);
        if (ufologist == null)
        {
            return NotFound(NoUfologistMessage);
        }

        var review = _mapper.Map<Review>(reviewCreateDto);
        review.Alien = alien;
        review.Ufologist = ufologist;

        var result = _reviewRepository.CreateReview(review);
        if (!result)
        {
            ModelState.AddModelError("", SaveErrorMessage);
            return StatusCode(500, ModelState);
        }

        return Ok(CreatedMessage);
    }

    [HttpPut]
    public IActionResult UpdateReview(ReviewUpdateDto reviewUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var review = _reviewRepository.GetReview(reviewUpdateDto.Id);
        if (review == null)
        {
            return NotFound(NotFoundMessage);
        }

        review.Title = reviewUpdateDto.Title;
        review.Text = reviewUpdateDto.Text;

        var result = _reviewRepository.UpdateReview(review);
        if (!result)
        {
            ModelState.AddModelError("", SaveErrorMessage);
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }

    [HttpDelete]
    public IActionResult DeleteReview(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var review = _reviewRepository.GetReview(id);
        if (review == null)
        {
            return NotFound(NotFoundMessage);
        }

        var result = _reviewRepository.DeleteReview(review);
        if (!result)
        {
            ModelState.AddModelError("", SaveErrorMessage);
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}