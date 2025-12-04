namespace DishFinder.Application.DTOs.Bookmarks;

public class BookmarkDto
{
    public int Id { get; set; }
    public int? RestaurantId { get; set; }
    public int? DishId { get; set; }
    public DateTime CreatedAt { get; set; }
}
