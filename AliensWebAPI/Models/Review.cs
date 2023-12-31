namespace AliensWebAPI.Models;

public class Review
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Text { get; set; } = "";
    public Ufologist Ufologist { get; set; } = null!;
    public Alien Alien { get; set; } = null!;
}