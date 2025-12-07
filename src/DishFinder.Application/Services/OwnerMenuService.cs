using DishFinder.Application.DTOs.Restaurants;
using DishFinder.Application.Interfaces;
using DishFinder.Application.Interfaces.Repositories;

namespace DishFinder.Application.Services;

public class OwnerMenuService : IOwnerMenuService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OwnerMenuService(IRestaurantRepository restaurantRepository, IMenuItemRepository menuItemRepository, IUnitOfWork unitOfWork)
    {
        _restaurantRepository = restaurantRepository;
        _menuItemRepository = menuItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<MenuItemDto>> GetMenuAsync(int restaurantId, Guid ownerId, CancellationToken cancellationToken = default)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId, cancellationToken);
        if (restaurant == null)
        {
            throw new KeyNotFoundException();
        }

        if (restaurant.OwnerUserId != ownerId)
        {
            throw new UnauthorizedAccessException();
        }

        var items = await _menuItemRepository.GetByRestaurantAsync(restaurantId, cancellationToken);
        return items.Select(m => new MenuItemDto
        {
            Id = m.Id,
            RestaurantId = m.RestaurantId,
            DishId = m.DishId,
            Price = m.Price,
            Currency = m.Currency,
            IsSignature = m.IsSignature,
            IsAvailable = m.IsAvailable
        });
    }

    public async Task<MenuItemDto> CreateAsync(Guid ownerId, CreateMenuItemDto dto, CancellationToken cancellationToken = default)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(dto.RestaurantId, cancellationToken);
        if (restaurant == null || restaurant.OwnerUserId != ownerId)
        {
            throw new UnauthorizedAccessException();
        }

        var item = new Domain.Entities.MenuItem
        {
            RestaurantId = dto.RestaurantId,
            DishId = dto.DishId,
            Price = dto.Price,
            Currency = dto.Currency,
            IsSignature = dto.IsSignature,
            IsAvailable = dto.IsAvailable
        };

        await _menuItemRepository.AddAsync(item, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new MenuItemDto
        {
            Id = item.Id,
            RestaurantId = item.RestaurantId,
            DishId = item.DishId,
            Price = item.Price,
            Currency = item.Currency,
            IsSignature = item.IsSignature,
            IsAvailable = item.IsAvailable
        };
    }

    public async Task<MenuItemDto?> UpdateAsync(int menuItemId, Guid ownerId, UpdateMenuItemDto dto, CancellationToken cancellationToken = default)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(menuItemId, cancellationToken);
        if (menuItem == null)
        {
            return null;
        }

        if (menuItem.Restaurant?.OwnerUserId != ownerId)
        {
            throw new UnauthorizedAccessException();
        }

        menuItem.Price = dto.Price;
        menuItem.Currency = dto.Currency;
        menuItem.IsSignature = dto.IsSignature;
        menuItem.IsAvailable = dto.IsAvailable;

        await _menuItemRepository.UpdateAsync(menuItem, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new MenuItemDto
        {
            Id = menuItem.Id,
            RestaurantId = menuItem.RestaurantId,
            DishId = menuItem.DishId,
            Price = menuItem.Price,
            Currency = menuItem.Currency,
            IsSignature = menuItem.IsSignature,
            IsAvailable = menuItem.IsAvailable
        };
    }

    public async Task<MenuItemDto?> ToggleAvailabilityAsync(int menuItemId, Guid ownerId, CancellationToken cancellationToken = default)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(menuItemId, cancellationToken);
        if (menuItem == null)
        {
            return null;
        }

        if (menuItem.Restaurant?.OwnerUserId != ownerId)
        {
            throw new UnauthorizedAccessException();
        }

        menuItem.IsAvailable = !menuItem.IsAvailable;
        await _menuItemRepository.UpdateAsync(menuItem, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new MenuItemDto
        {
            Id = menuItem.Id,
            RestaurantId = menuItem.RestaurantId,
            DishId = menuItem.DishId,
            Price = menuItem.Price,
            Currency = menuItem.Currency,
            IsSignature = menuItem.IsSignature,
            IsAvailable = menuItem.IsAvailable
        };
    }
}
