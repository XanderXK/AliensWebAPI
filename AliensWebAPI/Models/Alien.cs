namespace AliensWebAPI.Models;

public class Alien
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime ContactDate { get; set; }
    public Category Category { get; set; } = null!;
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<AlienSolarSystem>? SolarSystems { get; set; }
}