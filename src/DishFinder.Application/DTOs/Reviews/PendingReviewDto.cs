namespace DishFinder.Application.DTOs.Reviews;

public class PendingReviewDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public int Rating { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
