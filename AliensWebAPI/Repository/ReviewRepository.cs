using AliensWebAPI.Data;
using AliensWebAPI.Interfaces;
using AliensWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AliensWebAPI.Repository;

public class ReviewRepository : IReviewRepository
{
    private readonly DataContext _dataContext;

    public ReviewRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public ICollection<Review> GetReviews()
    {
        return _dataContext.Reviews.OrderBy(r => r.Id).ToList();
    }

    public Review GetReview(int id)
    {
        return _dataContext.Reviews.FirstOrDefault(r => r.Id == id);
    }

    public ICollection<Review> GetAlienReviews(int alienId)
    {
        return _dataContext.Aliens
            .Include(a => a.Reviews)
            .FirstOrDefault(a => a.Id == alienId).Reviews;
    }

    public bool CreateReview(Review review)
    {
        _dataContext.Add(review);
        return Save();
    }

    public bool UpdateReview(Review review)
    {
        _dataContext.Update(review);
        return Save();
    }

    public bool DeleteReview(Review review)
    {
        _dataContext.Remove(review);
        return Save();
    }

    public bool DeleteReviews(ICollection<Review> reviews)
    {
        _dataContext.RemoveRange(reviews);
        return Save();
    }

    public bool Save()
    {
        var saved = _dataContext.SaveChanges();
        return saved > 0;
    }
}