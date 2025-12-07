namespace DishFinder.Application.DTOs.Photos;

public class PendingPhotoDto
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public int? RestaurantId { get; set; }
    public string? RestaurantName { get; set; }
    public int? DishId { get; set; }
    public string? DishName { get; set; }
    public int? ReviewId { get; set; }
    public bool IsApproved { get; set; }
    public DateTime CreatedAt { get; set; }
}
