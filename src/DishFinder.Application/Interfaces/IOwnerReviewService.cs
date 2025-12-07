using DishFinder.Application.DTOs.Reviews;

namespace DishFinder.Application.Interfaces;

public interface IOwnerReviewService
{
    Task<IEnumerable<OwnerReviewDto>> GetForOwnerAsync(Guid ownerId, CancellationToken cancellationToken = default);
}
