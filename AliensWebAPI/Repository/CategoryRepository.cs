using AliensWebAPI.Data;
using AliensWebAPI.Interfaces;
using AliensWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AliensWebAPI.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext _dataContext;

    public CategoryRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public ICollection<Category> GetCategories()
    {
        return _dataContext.Categories.OrderBy(c => c.Id).ToList();
    }

    public Category? GetCategory(int id)
    {
        return _dataContext.Categories.FirstOrDefault(c => c.Id == id);
    }

    public Category? GetCategory(string name)
    {
        return _dataContext.Categories
            .FirstOrDefault(c => string.Equals(c.Name.ToUpper(), name.ToUpper()));
    }

    public ICollection<Alien> GetAliensByCategory(int id)
    {
        var category = GetCategory(id);
        if (category == null)
        {
            return new List<Alien>();
        }

        return _dataContext.Categories
            .Include(c => c.Aliens)
            .FirstOrDefault(c => c.Id == id)!.Aliens;
    }

    public bool CreateCategory(Category category)
    {
        _dataContext.Add(category);
        return Save();
    }
    
    public bool UpdateCategory(Category category)
    {
        _dataContext.Update(category);
        return Save();
    }

    public bool DeleteCategory(Category category)
    {
        _dataContext.Remove(category);
        return Save();
    }

    public bool Save()
    {
        var saved = _dataContext.SaveChanges();
        return saved > 0;
    }
}