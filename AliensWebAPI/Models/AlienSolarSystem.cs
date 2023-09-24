namespace AliensWebAPI.Models;

public class AlienSolarSystem
{
    public int AlienId { get; set; }
    public int SolarSystemId { get; set; }
    public Alien Alien { get; set; } = null!;
    public SolarSystem SolarSystem { get; set; } = null!;
}