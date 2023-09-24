namespace AliensWebAPI.Models;

public class Ufologist
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public ICollection<Review> Reviews { get; set; } = null!;
}