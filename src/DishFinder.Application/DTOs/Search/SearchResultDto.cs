namespace DishFinder.Application.DTOs.Search;

public class SearchResultDto
{
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; } = string.Empty;
    public int DishId { get; set; }
    public string DishName { get; set; } = string.Empty;
    public double AvgRating { get; set; }
    public int ReviewCount { get; set; }
    public decimal Price { get; set; }
    public double DistanceKm { get; set; }
    public bool IsSignatureDish { get; set; }
    public string? TopReviewSnippet { get; set; }
    public double Score { get; set; }
}
