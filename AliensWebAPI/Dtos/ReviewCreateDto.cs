namespace AliensWebAPI.Dtos;

public class ReviewCreateDto
{
    public required string Title { get; set; }
    public required string Text { get; set; }
    public required int AlienId { get; set; }
    public required int UfologistId { get; set; }
}