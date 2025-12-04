using AutoMapper;
using DishFinder.Application.Common;
using DishFinder.Application.DTOs.Restaurants;
using DishFinder.Application.Interfaces;
using DishFinder.Application.Interfaces.Repositories;
using DishFinder.Domain.Entities;

namespace DishFinder.Application.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RestaurantService(
        IRestaurantRepository restaurantRepository,
        IMenuItemRepository menuItemRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _menuItemRepository = menuItemRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<RestaurantDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id, cancellationToken);
        return restaurant == null ? null : _mapper.Map<RestaurantDto>(restaurant);
    }

    public async Task<IEnumerable<MenuItemDto>> GetMenuAsync(int restaurantId, CancellationToken cancellationToken = default)
    {
        var menu = await _menuItemRepository.GetByRestaurantAsync(restaurantId, cancellationToken);
        return menu.Select(_mapper.Map<MenuItemDto>);
    }

    public async Task<IEnumerable<RestaurantDto>> GetByAreaAsync(int areaId, CancellationToken cancellationToken = default)
    {
        var restaurants = await _restaurantRepository.GetByAreaAsync(areaId, cancellationToken);
        return restaurants.Select(_mapper.Map<RestaurantDto>);
    }

    public async Task<RestaurantDto> CreateAsync(RestaurantCreateDto dto, Guid ownerId, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<Restaurant>(dto);
        entity.CreatedAt = DateTime.UtcNow;
        await _restaurantRepository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<RestaurantDto>(entity);
    }

    public async Task<RestaurantDto?> UpdateAsync(int id, RestaurantCreateDto dto, Guid ownerId, CancellationToken cancellationToken = default)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id, cancellationToken);
        if (restaurant == null)
        {
            return null;
        }

        restaurant.Name = dto.Name;
        restaurant.Description = dto.Description;
        restaurant.Address = dto.Address;
        restaurant.AreaId = dto.AreaId;
        restaurant.Latitude = dto.Latitude;
        restaurant.Longitude = dto.Longitude;
        restaurant.IsVerified = dto.IsVerified;
        restaurant.UpdatedAt = DateTime.UtcNow;

        await _restaurantRepository.UpdateAsync(restaurant, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<RestaurantDto>(restaurant);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id, cancellationToken);
        if (restaurant == null)
        {
            return false;
        }

        await _restaurantRepository.DeleteAsync(restaurant, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
