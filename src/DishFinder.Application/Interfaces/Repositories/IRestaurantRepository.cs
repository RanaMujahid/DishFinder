using DishFinder.Domain.Entities;

namespace DishFinder.Application.Interfaces.Repositories;

public interface IRestaurantRepository
{
    Task<Restaurant?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Restaurant>> GetByAreaAsync(int areaId, CancellationToken cancellationToken = default);
    Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
    Task UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
    Task DeleteAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
}
