using AliensWebAPI.Models;

namespace AliensWebAPI.Interfaces;

public interface IUfologistRepository
{
    public ICollection<Ufologist> GetUfologists();
    public Ufologist? GetUfologist(int id);
    public ICollection<Review> GetUfologistReviews(int id);
    public bool CreateUfologst(Ufologist ufologist);
    public bool DeleteUfologst(Ufologist ufologist);
    public bool Save();
}