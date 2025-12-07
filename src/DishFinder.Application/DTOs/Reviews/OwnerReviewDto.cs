namespace DishFinder.Application.DTOs.Reviews;

public class OwnerReviewDto
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; } = string.Empty;
    public int DishId { get; set; }
    public string DishName { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int RatingTaste { get; set; }
    public int RatingPortion { get; set; }
    public int RatingValue { get; set; }
    public string? Comment { get; set; }
    public bool IsApproved { get; set; }
    public bool IsFlagged { get; set; }
    public DateTime CreatedAt { get; set; }
}
