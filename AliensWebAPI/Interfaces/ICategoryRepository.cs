using AliensWebAPI.Models;

namespace AliensWebAPI.Interfaces;

public interface ICategoryRepository
{
    public ICollection<Category> GetCategories();
    public Category? GetCategory(int id);
    public Category? GetCategory(string name);
    public ICollection<Alien> GetAliensByCategory(int id);
    public bool CreateCategory(Category category);
    public bool UpdateCategory(Category category);
    public bool DeleteCategory(Category category);
    public bool Save();
}