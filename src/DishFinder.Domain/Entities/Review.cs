namespace DishFinder.Domain.Entities;

public class Review
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
    public bool IsApproved { get; set; }
    public bool IsFlagged { get; set; }

    public User? User { get; set; }
    public Restaurant? Restaurant { get; set; }
    public Dish? Dish { get; set; }
    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
