namespace DishFinder.Domain.Entities;

public class Photo
{
    public int Id { get; set; }
    public int? ReviewId { get; set; }
    public int? RestaurantId { get; set; }
    public int? DishId { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsApproved { get; set; }
    public DateTime CreatedAt { get; set; }

    //public Review? Review { get; set; }
    //public Restaurant? Restaurant { get; set; }
    //public Dish? Dish { get; set; }
}
