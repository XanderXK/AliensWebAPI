namespace AliensWebAPI.Dtos;

public class ReviewCreateDto
{
    public string Title { get; set; } = "";
    public string Text { get; set; } = "";
    public int AlienId { get; set; }
    public int UfologistId { get; set; }
}