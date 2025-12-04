namespace DishFinder.Domain.Entities;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int AreaId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Phone { get; set; }
    public string? Website { get; set; }
    public bool IsVerified { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Area? Area { get; set; }
    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    public ICollection<OwnerRequest> OwnerRequests { get; set; } = new List<OwnerRequest>();
    public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
}
