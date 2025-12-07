namespace DishFinder.Application.DTOs.Reviews;

public class PendingReviewDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; } = string.Empty;
    public int DishId { get; set; }
    public string DishName { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public int RatingTaste { get; set; }
    public int RatingPortion { get; set; }
    public int RatingValue { get; set; }
    public bool IsApproved { get; set; }
    public bool IsFlagged { get; set; }
    public DateTime CreatedAt { get; set; }
}
