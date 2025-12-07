namespace DishFinder.Application.DTOs.Restaurants;

public class EditRestaurantDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Website { get; set; }
    public string? Phone { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
