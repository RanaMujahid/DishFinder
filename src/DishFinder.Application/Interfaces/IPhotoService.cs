using DishFinder.Application.Common;
using DishFinder.Application.DTOs.Photos;

namespace DishFinder.Application.Interfaces;

public interface IPhotoService
{
    Task<PhotoDto> UploadAsync(int? restaurantId, int? dishId, int? reviewId, string url, CancellationToken cancellationToken = default);
    Task<IEnumerable<PhotoDto>> GetForRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default);
    Task<IEnumerable<PhotoDto>> GetForDishAsync(int dishId, CancellationToken cancellationToken = default);
    Task<bool> ModerateAsync(int id, bool approve, CancellationToken cancellationToken = default);
}
