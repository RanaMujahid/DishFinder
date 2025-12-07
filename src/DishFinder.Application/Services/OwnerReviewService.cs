using DishFinder.Application.DTOs.Reviews;
using DishFinder.Application.Interfaces;
using DishFinder.Application.Interfaces.Repositories;

namespace DishFinder.Application.Services;

public class OwnerReviewService : IOwnerReviewService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IReviewRepository _reviewRepository;

    public OwnerReviewService(IRestaurantRepository restaurantRepository, IReviewRepository reviewRepository)
    {
        _restaurantRepository = restaurantRepository;
        _reviewRepository = reviewRepository;
    }

    public async Task<IEnumerable<OwnerReviewDto>> GetForOwnerAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        var restaurants = await _restaurantRepository.GetByOwnerAsync(ownerId, cancellationToken);
        var restaurantIds = restaurants.Select(r => r.Id).ToList();
        if (!restaurantIds.Any())
        {
            return Enumerable.Empty<OwnerReviewDto>();
        }

        var reviews = await _reviewRepository.GetForRestaurantsAsync(restaurantIds, cancellationToken);
        return reviews.Select(r => new OwnerReviewDto
        {
            Id = r.Id,
            RestaurantId = r.RestaurantId,
            RestaurantName = r.Restaurant?.Name ?? string.Empty,
            DishId = r.DishId,
            DishName = r.Dish?.Name ?? string.Empty,
            UserId = r.UserId,
            UserName = r.User?.Name ?? string.Empty,
            RatingTaste = r.RatingTaste,
            RatingPortion = r.RatingPortion,
            RatingValue = r.RatingValue,
            Comment = r.Comment,
            IsApproved = r.IsApproved,
            IsFlagged = r.IsFlagged,
            CreatedAt = r.CreatedAt
        });
    }
}
