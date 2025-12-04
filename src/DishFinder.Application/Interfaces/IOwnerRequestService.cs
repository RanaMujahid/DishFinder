using DishFinder.Application.DTOs.OwnerRequests;

namespace DishFinder.Application.Interfaces;

public interface IOwnerRequestService
{
    Task<OwnerRequestDto> CreateAsync(Guid userId, int restaurantId, CancellationToken cancellationToken = default);
    Task<IEnumerable<OwnerRequestDto>> GetPendingAsync(CancellationToken cancellationToken = default);
    Task<bool> ApproveAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> RejectAsync(int id, CancellationToken cancellationToken = default);
}
