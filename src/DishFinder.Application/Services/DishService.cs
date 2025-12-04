using AutoMapper;
using DishFinder.Application.DTOs.Restaurants;
using DishFinder.Application.Interfaces;
using DishFinder.Application.Interfaces.Repositories;

namespace DishFinder.Application.Services;

public class DishService : IDishService
{
    private readonly IDishRepository _dishRepository;
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IMapper _mapper;

    public DishService(IDishRepository dishRepository, IMenuItemRepository menuItemRepository, IMapper mapper)
    {
        _dishRepository = dishRepository;
        _menuItemRepository = menuItemRepository;
        _mapper = mapper;
    }

    public async Task<DishDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var dish = await _dishRepository.GetByIdAsync(id, cancellationToken);
        return dish == null ? null : _mapper.Map<DishDto>(dish);
    }

    public async Task<MenuItemDto?> GetRestaurantDishAsync(int restaurantId, int dishId, CancellationToken cancellationToken = default)
    {
        var menuItem = await _menuItemRepository.GetByRestaurantDishAsync(restaurantId, dishId, cancellationToken);
        return menuItem == null ? null : _mapper.Map<MenuItemDto>(menuItem);
    }
}
