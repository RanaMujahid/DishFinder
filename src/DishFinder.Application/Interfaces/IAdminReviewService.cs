using DishFinder.Application.DTOs.Reviews;

namespace DishFinder.Application.Interfaces;

public interface IAdminReviewService
{
    Task<IEnumerable<PendingReviewDto>> GetPendingAsync(CancellationToken cancellationToken = default);
    Task<bool> ApproveAsync(int reviewId, CancellationToken cancellationToken = default);
    Task<bool> RejectAsync(int reviewId, CancellationToken cancellationToken = default);
}
