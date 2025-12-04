namespace DishFinder.Domain.Entities;

public class Bookmark
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int? RestaurantId { get; set; }
    public int? DishId { get; set; }
    public DateTime CreatedAt { get; set; }

    public User? User { get; set; }
    public Restaurant? Restaurant { get; set; }
    public Dish? Dish { get; set; }
}
