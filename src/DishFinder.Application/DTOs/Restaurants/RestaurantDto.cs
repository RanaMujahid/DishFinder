namespace DishFinder.Application.DTOs.Restaurants;

public class RestaurantDto
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Image { get; set; } = "";
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
    public string Category { get; set; } = "";
    public string Area { get; set; } = "";
    public string Distance { get; set; } = "";
    public string PriceRange { get; set; } = "";
    public bool IsOpen { get; set; }
    public string OpeningHours { get; set; } = "";
    public string SignatureDish { get; set; } = "";
}
