using AutoMapper;
using DishFinder.Application.Common;
using DishFinder.Application.DTOs.Reviews;
using DishFinder.Application.Interfaces;
using DishFinder.Application.Interfaces.Repositories;
using DishFinder.Domain.Entities;

namespace DishFinder.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReviewService(IReviewRepository reviewRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResult<ReviewDto>> GetForDishAsync(int dishId, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var skip = (page - 1) * pageSize;
        var reviews = await _reviewRepository.GetForDishAsync(dishId, skip, pageSize, cancellationToken);
        var total = await _reviewRepository.CountForDishAsync(dishId, cancellationToken);

        return new PagedResult<ReviewDto>
        {
            Items = reviews.Select(_mapper.Map<ReviewDto>),
            Page = page,
            PageSize = pageSize,
            TotalItems = total,
            TotalPages = (int)Math.Ceiling(total / (double)pageSize)
        };
    }

    public async Task<ReviewDto> CreateAsync(Guid userId, ReviewCreateDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _reviewRepository.GetUserReviewAsync(userId, dto.RestaurantId, dto.DishId, cancellationToken);
        if (existing != null)
        {
            throw new InvalidOperationException("User has already reviewed this dish at the restaurant.");
        }

        var review = _mapper.Map<Review>(dto);
        review.UserId = userId;
        review.CreatedAt = DateTime.UtcNow;
        await _reviewRepository.AddAsync(review, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ReviewDto>(review);
    }

    public async Task<ReviewDto?> UpdateAsync(int id, Guid userId, ReviewCreateDto dto, CancellationToken cancellationToken = default)
    {
        var review = await _reviewRepository.GetByIdAsync(id, cancellationToken);
        if (review == null || review.UserId != userId)
        {
            return null;
        }

        review.RatingTaste = dto.RatingTaste;
        review.RatingPortion = dto.RatingPortion;
        review.RatingValue = dto.RatingValue;
        review.Comment = dto.Comment;
        review.RestaurantId = dto.RestaurantId;
        review.DishId = dto.DishId;

        await _reviewRepository.UpdateAsync(review, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ReviewDto>(review);
    }

    public async Task<bool> DeleteAsync(int id, Guid userId, CancellationToken cancellationToken = default)
    {
        var review = await _reviewRepository.GetByIdAsync(id, cancellationToken);
        if (review == null || review.UserId != userId)
        {
            return false;
        }

        await _reviewRepository.DeleteAsync(review, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
