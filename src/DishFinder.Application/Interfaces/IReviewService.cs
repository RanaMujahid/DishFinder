using DishFinder.Application.Common;
using DishFinder.Application.DTOs.Reviews;

namespace DishFinder.Application.Interfaces;

public interface IReviewService
{
    Task<PagedResult<ReviewDto>> GetForDishAsync(int dishId, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<ReviewDto> CreateAsync(Guid userId, ReviewCreateDto dto, CancellationToken cancellationToken = default);
    Task<ReviewDto?> UpdateAsync(int id, Guid userId, ReviewCreateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, Guid userId, CancellationToken cancellationToken = default);
}
