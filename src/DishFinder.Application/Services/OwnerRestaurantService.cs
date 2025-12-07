using DishFinder.Application.DTOs.Restaurants;
using DishFinder.Application.Interfaces;
using DishFinder.Application.Interfaces.Repositories;

namespace DishFinder.Application.Services;

public class OwnerRestaurantService : IOwnerRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OwnerRestaurantService(IRestaurantRepository restaurantRepository, IUnitOfWork unitOfWork)
    {
        _restaurantRepository = restaurantRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OwnerRestaurantDto>> GetMyRestaurantsAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        var restaurants = await _restaurantRepository.GetByOwnerAsync(ownerId, cancellationToken);
        return restaurants.Select(r => new OwnerRestaurantDto
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            Address = r.Address,
            AreaId = r.AreaId,
            Website = r.Website,
            Phone = r.Phone,
            Latitude = r.Latitude,
            Longitude = r.Longitude,
            IsVerified = r.IsVerified
        });
    }

    public async Task<OwnerRestaurantDto?> UpdateAsync(int restaurantId, Guid ownerId, EditRestaurantDto dto, CancellationToken cancellationToken = default)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId, cancellationToken);
        if (restaurant == null)
        {
            return null;
        }

        if (restaurant.OwnerUserId != ownerId)
        {
            throw new UnauthorizedAccessException();
        }

        restaurant.Name = dto.Name;
        restaurant.Description = dto.Description;
        restaurant.Address = dto.Address;
        restaurant.Website = dto.Website;
        restaurant.Phone = dto.Phone;
        restaurant.Latitude = dto.Latitude;
        restaurant.Longitude = dto.Longitude;
        restaurant.UpdatedAt = DateTime.UtcNow;

        await _restaurantRepository.UpdateAsync(restaurant, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new OwnerRestaurantDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Description = restaurant.Description,
            Address = restaurant.Address,
            AreaId = restaurant.AreaId,
            Website = restaurant.Website,
            Phone = restaurant.Phone,
            Latitude = restaurant.Latitude,
            Longitude = restaurant.Longitude,
            IsVerified = restaurant.IsVerified
        };
    }
}
