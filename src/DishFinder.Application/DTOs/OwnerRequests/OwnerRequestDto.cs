using DishFinder.Domain.Enums;

namespace DishFinder.Application.DTOs.OwnerRequests;

public class OwnerRequestDto
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public Guid UserId { get; set; }
    public OwnerRequestStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
