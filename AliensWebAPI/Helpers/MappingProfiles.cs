using AliensWebAPI.Dtos;
using AliensWebAPI.Models;
using AutoMapper;

namespace AliensWebAPI.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Alien, AlienDto>()
            .ForMember(dto => dto.CategoryName, options => options
                .MapFrom(alien => alien.Category.Name));

        CreateMap<AlienDto, Alien>();

        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();
        CreateMap<CategoryCreateDto, Category>();

        CreateMap<Review, ReviewDto>();
        CreateMap<ReviewCreateDto, Review>();

        CreateMap<SolarSystem, SolarSystemDto>();
        CreateMap<SolarSystemDto, SolarSystem>();

        CreateMap<Ufologist, UfologistDto>()
            .ForMember(dto => dto.ReviewsCount, options => options
                .MapFrom(ufologist => ufologist.Reviews.Count));

        CreateMap<UfologistCreateDto, Ufologist>();
    }
}