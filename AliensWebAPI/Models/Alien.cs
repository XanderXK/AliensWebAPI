namespace AliensWebAPI.Models;

public class Alien
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime ContactDate { get; set; }
    public Category Category { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<AlienSolarSystem> SolarSystems { get; set; }
}