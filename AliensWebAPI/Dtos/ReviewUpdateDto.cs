namespace AliensWebAPI.Dtos;

public class ReviewUpdateDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Text { get; set; } = "";
}