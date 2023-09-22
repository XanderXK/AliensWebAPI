namespace AliensWebAPI.Models;

public class AlienSolarSystem
{
    public int AlienId { get; set; }
    public int SolarSystemId { get; set; }
    public Alien Alien { get; set; }
    public SolarSystem SolarSystem { get; set; }
}