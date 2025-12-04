namespace DishFinder.Domain.Entities;

public class Dish
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
}
