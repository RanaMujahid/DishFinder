namespace DishFinder.Application.DTOs.Restaurants;

public class UpdateMenuItemDto
{
    public decimal Price { get; set; }
    public string Currency { get; set; } = "AED";
    public bool IsSignature { get; set; }
    public bool IsAvailable { get; set; }
}
