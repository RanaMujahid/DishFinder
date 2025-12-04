using DishFinder.Application.Common;
using DishFinder.Application.DTOs.Search;
using DishFinder.Application.Interfaces;
using DishFinder.Application.Interfaces.Repositories;
using DishFinder.Domain.Entities;

namespace DishFinder.Application.Services;

public class SearchService : ISearchService
{
    private readonly IRestaurantRepository _restaurants;
    private readonly IDishRepository _dishes;
    private readonly IMenuItemRepository _menuItems;
    private readonly IReviewRepository _reviews;

    public SearchService(IRestaurantRepository restaurants, IDishRepository dishes, IMenuItemRepository menuItems, IReviewRepository reviews)
    {
        _restaurants = restaurants;
        _dishes = dishes;
        _menuItems = menuItems;
        _reviews = reviews;
    }

    public async Task<PagedResult<SearchResultDto>> SearchAsync(string dish, string area, int page, int pageSize, double? lat = null, double? lng = null, CancellationToken cancellationToken = default)
    {
        // This simplified search assumes dishes were loaded and matched by name
        var allDishes = await _dishes.GetAllAsync(cancellationToken);
        var matchedDishes = allDishes.Where(d => d.Name.Contains(dish, StringComparison.OrdinalIgnoreCase));

        var results = new List<SearchResultDto>();
        foreach (var d in matchedDishes)
        {
            // In a full implementation we would filter restaurants by area and join menu items
            var menuItem = await _menuItems.GetByRestaurantDishAsync(1, d.Id, cancellationToken); // placeholder
            if (menuItem == null) continue;

            var reviewCount = await _reviews.CountForDishAsync(d.Id, cancellationToken);
            var reviews = await _reviews.GetForDishAsync(d.Id, 0, 1, cancellationToken);
            var avgRating = reviewCount > 0 ? reviews.Average(r => (r.RatingTaste + r.RatingPortion + r.RatingValue) / 3.0) : 0;
            var distance = CalculateDistanceKm(lat ?? 0, lng ?? 0, 0, 0);
            var score = (avgRating * 0.6) + (Math.Log(reviewCount + 1) * 0.3) + ((1.0 / (distance + 1)) * 0.1) + (menuItem.IsSignature ? 0.05 : 0);

            results.Add(new SearchResultDto
            {
                DishId = d.Id,
                DishName = d.Name,
                RestaurantId = menuItem.RestaurantId,
                RestaurantName = "Restaurant",
                AvgRating = avgRating,
                ReviewCount = reviewCount,
                Price = menuItem.Price,
                DistanceKm = distance,
                IsSignatureDish = menuItem.IsSignature,
                TopReviewSnippet = reviews.FirstOrDefault()?.Comment,
                Score = score
            });
        }

        var ordered = results.OrderByDescending(r => r.Score).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new PagedResult<SearchResultDto>
        {
            Items = ordered,
            Page = page,
            PageSize = pageSize,
            TotalItems = results.Count,
            TotalPages = (int)Math.Ceiling(results.Count / (double)pageSize)
        };
    }

    private static double CalculateDistanceKm(double lat1, double lon1, double lat2, double lon2)
    {
        double R = 6371;
        double dLat = (lat2 - lat1) * Math.PI / 180;
        double dLon = (lon2 - lon1) * Math.PI / 180;
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }
}
