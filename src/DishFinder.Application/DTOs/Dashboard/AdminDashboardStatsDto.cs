namespace DishFinder.Application.DTOs.Dashboard;

public class AdminDashboardStatsDto
{
    public int PendingReviews { get; set; }
    public int PendingPhotos { get; set; }
    public int PendingOwnerRequests { get; set; }
    public int PlaggedReviews { get; set; }
}
