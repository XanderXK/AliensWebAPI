namespace AliensWebAPI.Dtos;

public class ReviewUpdateDto
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Text { get; set; }
}