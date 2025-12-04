namespace DishFinder.Application.DTOs.Reviews;

public class ReviewDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int RestaurantId { get; set; }
    public int DishId { get; set; }
    public int RatingTaste { get; set; }
    public int RatingPortion { get; set; }
    public int RatingValue { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}
