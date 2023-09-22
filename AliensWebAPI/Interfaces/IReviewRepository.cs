using AliensWebAPI.Models;

namespace AliensWebAPI.Interfaces;

public interface IReviewRepository
{
    public ICollection<Review> GetReviews();
    public Review? GetReview(int id);
    public ICollection<Review> GetAlienReviews(int alienId);
    public bool CreateReview(Review review);
    public bool UpdateReview(Review review);
    public bool DeleteReview(Review review);
    public bool Save();
}