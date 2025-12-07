using DishFinder.Application.DTOs.Restaurants;

namespace DishFinder.Application.Interfaces;

public interface IOwnerMenuService
{
    Task<IEnumerable<MenuItemDto>> GetMenuAsync(int restaurantId, Guid ownerId, CancellationToken cancellationToken = default);
    Task<MenuItemDto> CreateAsync(Guid ownerId, CreateMenuItemDto dto, CancellationToken cancellationToken = default);
    Task<MenuItemDto?> UpdateAsync(int menuItemId, Guid ownerId, UpdateMenuItemDto dto, CancellationToken cancellationToken = default);
    Task<MenuItemDto?> ToggleAvailabilityAsync(int menuItemId, Guid ownerId, CancellationToken cancellationToken = default);
}
