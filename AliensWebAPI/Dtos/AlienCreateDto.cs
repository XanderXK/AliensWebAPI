namespace AliensWebAPI.Dtos;

public class AlienCreateDto
{
    public string Name { get; set; } = "";
    public string CategoryName { get; set; } = "";
    public DateTime ContactDate { get; set; }
    public ICollection<int>? SolarSystemIds { get; set; }
}