namespace AliensWebAPI.Dtos;

public class SolarSystemDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Address { get; set; }
}