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

    public async Task<IEnumerable<RestaurantDto>> GetFeaturedAsync(CancellationToken cancellationToken = default)
    {
        return new List<RestaurantDto>()
    {
        new RestaurantDto {
            Id = "1",
            Name = "Al Safadi",
            Image = "https://images.unsplash.com/photo-1555939594-58d7cb561ad1?w=800",
            Rating = 4.7,
            ReviewCount = 2834,
            Category = "Lebanese",
            Area = "Jumeirah",
            Distance = "1.2 km",
            PriceRange = "$$",
            IsOpen = true,
            OpeningHours = "10:00 AM - 12:00 AM",
            SignatureDish = "Mixed Grill Platter"
        },
        new RestaurantDto {
            Id = "2",
            Name = "Ravi Restaurant",
            Image = "https://images.unsplash.com/photo-1631515243349-e0cb75fb8d3a?w=800",
            Rating = 4.5,
            ReviewCount = 5621,
            Category = "Pakistani",
            Area = "Satwa",
            Distance = "2.5 km",
            PriceRange = "$",
            IsOpen = true,
            OpeningHours = "5:00 AM - 3:00 AM",
            SignatureDish = "Chicken Karahi"
        },
        new RestaurantDto {
            Id = "3",
            Name = "Bu Qtair",
            Image = "https://images.unsplash.com/photo-1504674900247-0877df9cc836?w=800",
            Rating = 4.8,
            ReviewCount = 3456,
            Category = "Seafood",
            Area = "Jumeirah Beach",
            Distance = "4.1 km",
            PriceRange = "$$",
            IsOpen = false,
            OpeningHours = "11:30 AM - 11:00 PM",
            SignatureDish = "Hammour Fish"
        },
        new RestaurantDto {
            Id = "4",
            Name = "Zaroob",
            Image = "https://images.unsplash.com/photo-1529006557810-274b9b2fc783?w=800",
            Rating = 4.4,
            ReviewCount = 1892,
            Category = "Levantine",
            Area = "Downtown Dubai",
            Distance = "3.2 km",
            PriceRange = "$",
            IsOpen = true,
            OpeningHours = "8:00 AM - 2:00 AM",
            SignatureDish = "Shawarma Wrap"
        },
        new RestaurantDto {
            Id = "5",
            Name = "Tresind Studio",
            Image = "https://images.unsplash.com/photo-1517248135467-4c7edcad34c4?w=800",
            Rating = 4.9,
            ReviewCount = 876,
            Category = "Indian Fine Dining",
            Area = "DIFC",
            Distance = "5.8 km",
            PriceRange = "$$$$",
            IsOpen = true,
            OpeningHours = "7:00 PM - 11:30 PM",
            SignatureDish = "Molecular Biryani"
        },
        new RestaurantDto {
            Id = "6",
            Name = "Operation Falafel",
            Image = "https://images.unsplash.com/photo-1565557623262-b51c2513a641?w=800",
            Rating = 4.3,
            ReviewCount = 2145,
            Category = "Middle Eastern",
            Area = "JLT",
            Distance = "6.3 km",
            PriceRange = "$",
            IsOpen = true,
            OpeningHours = "9:00 AM - 1:00 AM",
            SignatureDish = "Falafel Platter"
        }
    };
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
