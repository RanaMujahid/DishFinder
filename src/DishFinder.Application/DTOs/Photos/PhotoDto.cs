namespace DishFinder.Application.DTOs.Photos;

public class PhotoDto
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsApproved { get; set; }
    public DateTime CreatedAt { get; set; }
}
