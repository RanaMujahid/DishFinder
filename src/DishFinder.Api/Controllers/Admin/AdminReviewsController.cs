using DishFinder.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DishFinder.Api.Controllers.Admin;

[ApiController]
[Route("admin/reviews")]
[Authorize(Roles = "Admin")]
public class AdminReviewsController : ControllerBase
{
    private readonly IAdminReviewService _adminReviewService;

    public AdminReviewsController(IAdminReviewService adminReviewService)
    {
        _adminReviewService = adminReviewService;
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPending(CancellationToken cancellationToken)
    {
        var reviews = await _adminReviewService.GetPendingAsync(cancellationToken);
        return Ok(reviews);
    }

    [HttpPost("{id:int}/approve")]
    public async Task<IActionResult> Approve(int id, CancellationToken cancellationToken)
    {
        var approved = await _adminReviewService.ApproveAsync(id, cancellationToken);
        return approved ? NoContent() : NotFound();
    }

    [HttpPost("{id:int}/reject")]
    public async Task<IActionResult> Reject(int id, CancellationToken cancellationToken)
    {
        var rejected = await _adminReviewService.RejectAsync(id, cancellationToken);
        return rejected ? NoContent() : NotFound();
    }
}
