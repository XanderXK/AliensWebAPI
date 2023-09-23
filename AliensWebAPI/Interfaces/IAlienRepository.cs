using AliensWebAPI.Models;

namespace AliensWebAPI.Interfaces;

public interface IAlienRepository
{
    public ICollection<Alien> GetAliens();
    public Alien? GetAlien(int id);
    public int GetAlienRating(int id);
    public bool CreateAlien(Alien alien, ICollection<SolarSystem>? solarSystems);
    public bool UpdateAlien(Alien alien);
    public bool DeleteAlien(Alien alien);
    public bool Save();
}