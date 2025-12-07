using DishFinder.Application.DTOs.OwnerRequests;

namespace DishFinder.Application.Interfaces;

public interface IAdminOwnerRequestService
{
    Task<IEnumerable<OwnerRequestDto>> GetPendingAsync(CancellationToken cancellationToken = default);
    Task<bool> ApproveAsync(int requestId, CancellationToken cancellationToken = default);
    Task<bool> RejectAsync(int requestId, CancellationToken cancellationToken = default);
}
