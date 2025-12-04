using DishFinder.Application.DTOs.Restaurants;
using DishFinder.Application.Common;

namespace DishFinder.Application.Interfaces;

public interface IRestaurantService
{
    Task<RestaurantDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<MenuItemDto>> GetMenuAsync(int restaurantId, CancellationToken cancellationToken = default);
    Task<IEnumerable<RestaurantDto>> GetByAreaAsync(int areaId, CancellationToken cancellationToken = default);
    Task<RestaurantDto> CreateAsync(RestaurantCreateDto dto, Guid ownerId, CancellationToken cancellationToken = default);
    Task<RestaurantDto?> UpdateAsync(int id, RestaurantCreateDto dto, Guid ownerId, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
