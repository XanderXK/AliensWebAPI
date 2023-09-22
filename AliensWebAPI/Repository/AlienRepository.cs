using AliensWebAPI.Data;
using AliensWebAPI.Interfaces;
using AliensWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AliensWebAPI.Repository;

public class AlienRepository : IAlienRepository
{
    private readonly DataContext _dataContext;

    public AlienRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public ICollection<Alien> GetAliens()
    {
        return _dataContext.Aliens.Include(a=>a.Category).OrderBy(alien => alien.Id).ToList();
    }

    public Alien? GetAlien(int id)
    {
        var alien = _dataContext.Aliens.Include(a=>a.Category).FirstOrDefault(a => a.Id == id);
        return alien;
    }

    public Alien GetAlien(string name)
    {
        var alien = _dataContext.Aliens.Include(a=>a.Category).FirstOrDefault(a => a.Name == name);
        return alien;
    }

    public int GetAlienRating(int id)
    {
        var reviews = _dataContext.Reviews.Where(r => r.Alien.Id == id);
        return reviews.Count();
    }

    public bool CreateAlien(Alien alien)
    {
        _dataContext.Add(alien);
        return Save();
    }

    public bool UpdateAlien(Alien alien)
    {
        _dataContext.Update(alien);
        return Save();
    }

    public bool DeleteAlien(Alien alien)
    {
        _dataContext.Remove(alien);
        return Save();
    }

    public bool Save()
    {
        var saved = _dataContext.SaveChanges();
        return saved > 0;
    }
}