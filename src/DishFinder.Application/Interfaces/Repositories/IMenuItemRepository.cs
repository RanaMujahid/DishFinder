using DishFinder.Domain.Entities;

namespace DishFinder.Application.Interfaces.Repositories;

public interface IMenuItemRepository
{
    Task<IEnumerable<MenuItem>> GetByRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default);
    Task<MenuItem?> GetByRestaurantDishAsync(int restaurantId, int dishId, CancellationToken cancellationToken = default);
    Task<MenuItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(MenuItem item, CancellationToken cancellationToken = default);
    Task UpdateAsync(MenuItem item, CancellationToken cancellationToken = default);
    Task DeleteAsync(MenuItem item, CancellationToken cancellationToken = default);
}
