using DishFinder.Application.DTOs.Dashboard;
using DishFinder.Application.DTOs.Reviews;

namespace DishFinder.Application.Interfaces;

public interface IAdminDashboardService
{
    Task<AdminDashboardStatsDto> GetAllStatsAsync(CancellationToken cancellationToken = default);
}
