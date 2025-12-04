using DishFinder.Domain.Enums;

namespace DishFinder.Domain.Entities;

public class OwnerRequest
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int RestaurantId { get; set; }
    public OwnerRequestStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public User? User { get; set; }
    public Restaurant? Restaurant { get; set; }
}
