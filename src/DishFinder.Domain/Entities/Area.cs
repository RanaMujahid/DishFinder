namespace DishFinder.Domain.Entities;

public class Area
{
    public int Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}
