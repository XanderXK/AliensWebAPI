namespace AliensWebAPI.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public ICollection<Alien> Aliens { get; set; } = new List<Alien>();
}