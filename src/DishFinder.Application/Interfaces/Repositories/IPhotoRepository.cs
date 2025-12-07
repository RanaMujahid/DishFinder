using DishFinder.Domain.Entities;

namespace DishFinder.Application.Interfaces.Repositories;

public interface IPhotoRepository
{
    Task AddAsync(Photo photo, CancellationToken cancellationToken = default);
    Task<Photo?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Photo>> GetForRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Photo>> GetForDishAsync(int dishId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Photo>> GetPendingAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(Photo photo, CancellationToken cancellationToken = default);
    Task DeleteAsync(Photo photo, CancellationToken cancellationToken = default);
}
