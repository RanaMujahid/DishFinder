namespace DishFinder.Domain.Entities;

public class MenuItem
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public int DishId { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = "AED";
    public bool IsSignature { get; set; }
    public bool IsAvailable { get; set; }

    public Restaurant? Restaurant { get; set; }
    public Dish? Dish { get; set; }
}
