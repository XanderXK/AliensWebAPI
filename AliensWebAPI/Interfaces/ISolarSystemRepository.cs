using AliensWebAPI.Models;

namespace AliensWebAPI.Interfaces;

public interface ISolarSystemRepository
{
    public ICollection<SolarSystem> GetSolarSystems();
    public SolarSystem? GetSolarSystem(int id);
    public ICollection<Alien> GetAliensInSystem(int solarSystemId);
    public bool CreateSolarSystem(SolarSystem solarSystem);
    public bool UpdateSolarSystem(SolarSystem solarSystem);
    public bool Save();
}