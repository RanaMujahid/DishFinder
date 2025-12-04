using DishFinder.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DishFinder.Api.Controllers;

[ApiController]
[Route("")]
public class OwnerRequestsController : ControllerBase
{
    private readonly IOwnerRequestService _ownerRequestService;

    public OwnerRequestsController(IOwnerRequestService ownerRequestService)
    {
        _ownerRequestService = ownerRequestService;
    }

    [HttpPost("restaurant/claim")]
    [Authorize]
    public async Task<IActionResult> Claim([FromForm] int restaurantId, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst("sub")!.Value);
        var request = await _ownerRequestService.CreateAsync(userId, restaurantId, cancellationToken);
        return Ok(request);
    }

    [HttpGet("admin/owner-requests")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetPending(CancellationToken cancellationToken)
    {
        var requests = await _ownerRequestService.GetPendingAsync(cancellationToken);
        return Ok(requests);
    }

    [HttpPut("admin/owner-requests/{id:int}/approve")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Approve(int id, CancellationToken cancellationToken)
    {
        var approved = await _ownerRequestService.ApproveAsync(id, cancellationToken);
        return approved ? NoContent() : NotFound();
    }

    [HttpPut("admin/owner-requests/{id:int}/reject")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Reject(int id, CancellationToken cancellationToken)
    {
        var rejected = await _ownerRequestService.RejectAsync(id, cancellationToken);
        return rejected ? NoContent() : NotFound();
    }
}
