using System.ComponentModel.DataAnnotations;

namespace AliensWebAPI.Dtos;

public class AlienDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    [Range(typeof(DateTime), "2000-01-01", "9999-12-31")]
    public DateTime ContactDate { get; set; }

    public required string CategoryName { get; set; }
    public ICollection<int>? SolarSystemIds { get; set; }
}