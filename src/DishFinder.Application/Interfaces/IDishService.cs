using DishFinder.Application.DTOs.Restaurants;

namespace DishFinder.Application.Interfaces;

public interface IDishService
{
    Task<DishDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<MenuItemDto?> GetRestaurantDishAsync(int restaurantId, int dishId, CancellationToken cancellationToken = default);
}
