using System.ComponentModel.DataAnnotations;

namespace AliensWebAPI.Dtos;

public class AlienCreateDto
{
    public required string Name { get; set; }
    public required string CategoryName { get; set; }

    [Range(typeof(DateTime), "2000-01-01", "9999-12-31")]
    public required DateTime ContactDate { get; set; }

    public ICollection<int>? SolarSystemIds { get; set; }
}