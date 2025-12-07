using DishFinder.Domain.Entities;

namespace DishFinder.Application.Interfaces.Repositories;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetForDishAsync(int dishId, int skip, int take, CancellationToken cancellationToken = default);
    Task<int> CountForDishAsync(int dishId, CancellationToken cancellationToken = default);
    Task<Review?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Review>> GetPendingAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Review>> GetForRestaurantsAsync(IEnumerable<int> restaurantIds, CancellationToken cancellationToken = default);
    Task<Review?> GetUserReviewAsync(Guid userId, int restaurantId, int dishId, CancellationToken cancellationToken = default);
    Task AddAsync(Review review, CancellationToken cancellationToken = default);
    Task UpdateAsync(Review review, CancellationToken cancellationToken = default);
    Task DeleteAsync(Review review, CancellationToken cancellationToken = default);
}
