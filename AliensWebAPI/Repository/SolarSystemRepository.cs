using AliensWebAPI.Data;
using AliensWebAPI.Interfaces;
using AliensWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AliensWebAPI.Repository;

public class SolarSystemRepository : ISolarSystemRepository
{
    private readonly DataContext _dataContext;

    public SolarSystemRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public ICollection<SolarSystem> GetSolarSystems()
    {
        return _dataContext.SolarSystems.OrderBy(s => s.Id).ToList();
    }

    public SolarSystem? GetSolarSystem(int id)
    {
        return _dataContext.SolarSystems.FirstOrDefault(s => s.Id == id);
    }

    public ICollection<Alien> GetAliensInSystem(int solarSystemId)
    {
        return _dataContext.AliensSolarSystems.Where(s => s.SolarSystemId == solarSystemId)
            .Include(s => s.Alien)
            .Include(a => a.Alien.Category)
            .Select(s => s.Alien).ToList();
    }

    public bool CreateSolarSystem(SolarSystem solarSystem)
    {
        _dataContext.SolarSystems.Add(solarSystem);
        var result = Save();
        return result;
    }

    public bool UpdateSolarSystem(SolarSystem solarSystem)
    {
        _dataContext.SolarSystems.Update(solarSystem);
        var result = Save();
        return result;
    }

    public bool Save()
    {
        var saved = _dataContext.SaveChanges();
        return saved > 0;
    }
}