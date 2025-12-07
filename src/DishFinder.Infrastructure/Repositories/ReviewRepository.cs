using DishFinder.Application.Interfaces.Repositories;
using DishFinder.Domain.Entities;
using DishFinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DishFinder.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _db;
    public ReviewRepository(ApplicationDbContext db) => _db = db;

    public async Task AddAsync(Review review, CancellationToken cancellationToken = default)
    {
        await _db.Reviews.AddAsync(review, cancellationToken);
    }

    public Task<int> CountForDishAsync(int dishId, CancellationToken cancellationToken = default)
        => _db.Reviews.CountAsync(r => r.DishId == dishId, cancellationToken);

    public Task DeleteAsync(Review review, CancellationToken cancellationToken = default)
    {
        _db.Reviews.Remove(review);
        return Task.CompletedTask;
    }

    public Task<Review?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => _db.Reviews.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task<IEnumerable<Review>> GetPendingAsync(CancellationToken cancellationToken = default)
        => await _db.Reviews
            .Include(r => r.User)
            .Include(r => r.Restaurant)
            .Include(r => r.Dish)
            .Where(r => !r.IsApproved || r.IsFlagged)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Review>> GetForRestaurantsAsync(IEnumerable<int> restaurantIds, CancellationToken cancellationToken = default)
        => await _db.Reviews
            .Include(r => r.User)
            .Include(r => r.Restaurant)
            .Include(r => r.Dish)
            .Where(r => restaurantIds.Contains(r.RestaurantId))
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Review>> GetForDishAsync(int dishId, int skip, int take, CancellationToken cancellationToken = default)
        => await _db.Reviews.Where(r => r.DishId == dishId).OrderByDescending(r => r.CreatedAt).Skip(skip).Take(take).ToListAsync(cancellationToken);

    public Task<Review?> GetUserReviewAsync(Guid userId, int restaurantId, int dishId, CancellationToken cancellationToken = default)
        => _db.Reviews.FirstOrDefaultAsync(r => r.UserId == userId && r.RestaurantId == restaurantId && r.DishId == dishId, cancellationToken);

    public Task UpdateAsync(Review review, CancellationToken cancellationToken = default)
    {
        _db.Reviews.Update(review);
        return Task.CompletedTask;
    }
}
