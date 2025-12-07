using DishFinder.Application.DTOs.Restaurants;

namespace DishFinder.Application.Interfaces;

public interface IOwnerRestaurantService
{
    Task<IEnumerable<OwnerRestaurantDto>> GetMyRestaurantsAsync(Guid ownerId, CancellationToken cancellationToken = default);
    Task<OwnerRestaurantDto?> UpdateAsync(int restaurantId, Guid ownerId, EditRestaurantDto dto, CancellationToken cancellationToken = default);
}
