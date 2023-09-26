using AliensWebAPI.Data;
using AliensWebAPI.Interfaces;
using AliensWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AliensWebAPI.Repository;

public class UfologistRepository : IUfologistRepository
{
    private readonly DataContext _dataContext;

    public UfologistRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public ICollection<Ufologist> GetUfologists()
    {
        return _dataContext.Ufologists
            .Include(ufologist => ufologist.Reviews)
            .OrderBy(ufologist => ufologist.Id).ToList();
    }

    public Ufologist? GetUfologist(int id)
    {
        return _dataContext.Ufologists
            .Include(ufologist => ufologist.Reviews)
            .FirstOrDefault(ufologist => ufologist.Id == id);
    }

    public ICollection<Review> GetUfologistReviews(int id)
    {
        return _dataContext.Ufologists
            .Include(ufologist => ufologist.Reviews)
            .FirstOrDefault(ufologist => ufologist.Id == id)!
            .Reviews;
    }

    public bool CreateUfologst(Ufologist ufologist)
    {
        _dataContext.Ufologists.Add(ufologist);
        var result = Save();
        return result;
    }

    public bool DeleteUfologst(Ufologist ufologist)
    {
        _dataContext.Remove(ufologist);
        return Save();
    }

    public bool Save()
    {
        var saved = _dataContext.SaveChanges();
        return saved > 0;
    }
}