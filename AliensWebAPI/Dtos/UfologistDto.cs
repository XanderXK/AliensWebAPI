namespace AliensWebAPI.Dtos;

public class UfologistDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int ReviewsCount { get; set; }
}