using DishFinder.Application.DTOs.Reviews;
using DishFinder.Application.Interfaces;
using DishFinder.Application.Interfaces.Repositories;

namespace DishFinder.Application.Services;

public class AdminReviewService : IAdminReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdminReviewService(IReviewRepository reviewRepository, IUnitOfWork unitOfWork)
    {
        _reviewRepository = reviewRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PendingReviewDto>> GetPendingAsync(CancellationToken cancellationToken = default)
    {
        var reviews = await _reviewRepository.GetPendingAsync(cancellationToken);

        return reviews.Select(r => new PendingReviewDto
        {
            Id = r.Id,
            UserId = r.UserId,
            UserName = r.User?.Name ?? string.Empty,
            RestaurantId = r.RestaurantId,
            RestaurantName = r.Restaurant?.Name ?? string.Empty,
            DishId = r.DishId,
            DishName = r.Dish?.Name ?? string.Empty,
            Comment = r.Comment,
            RatingTaste = r.RatingTaste,
            RatingPortion = r.RatingPortion,
            RatingValue = r.RatingValue,
            IsApproved = r.IsApproved,
            IsFlagged = r.IsFlagged,
            CreatedAt = r.CreatedAt
        });
    }

    public async Task<bool> ApproveAsync(int reviewId, CancellationToken cancellationToken = default)
    {
        var review = await _reviewRepository.GetByIdAsync(reviewId, cancellationToken);
        if (review == null)
        {
            return false;
        }

        review.IsApproved = true;
        review.IsFlagged = false;
        await _reviewRepository.UpdateAsync(review, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> RejectAsync(int reviewId, CancellationToken cancellationToken = default)
    {
        var review = await _reviewRepository.GetByIdAsync(reviewId, cancellationToken);
        if (review == null)
        {
            return false;
        }

        await _reviewRepository.DeleteAsync(review, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
