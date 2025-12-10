using DishFinder.Application.DTOs.Dashboard;
using DishFinder.Application.DTOs.Reviews;
using DishFinder.Application.Interfaces;
using DishFinder.Application.Interfaces.Repositories;

namespace DishFinder.Application.Services;

public class AdminDahsboardService : IAdminDashboardService
{
    private readonly IReviewRepository _reviewRepository;

    public AdminDahsboardService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<AdminDashboardStatsDto> GetAllStatsAsync(CancellationToken cancellationToken = default)
    {

        return new AdminDashboardStatsDto
        {
            PendingOwnerRequests = 1,
            PendingPhotos = 5,
            PendingReviews = 4,
            PlaggedReviews = 3
        };
    }

}
