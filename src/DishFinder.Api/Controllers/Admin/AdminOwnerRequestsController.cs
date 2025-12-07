using DishFinder.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DishFinder.Api.Controllers.Admin;

[ApiController]
[Route("admin/owner-requests")]
[Authorize(Roles = "Admin")]
public class AdminOwnerRequestsController : ControllerBase
{
    private readonly IAdminOwnerRequestService _adminOwnerRequestService;

    public AdminOwnerRequestsController(IAdminOwnerRequestService adminOwnerRequestService)
    {
        _adminOwnerRequestService = adminOwnerRequestService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPending(CancellationToken cancellationToken)
    {
        var requests = await _adminOwnerRequestService.GetPendingAsync(cancellationToken);
        return Ok(requests);
    }

    [HttpPost("{id:int}/approve")]
    public async Task<IActionResult> Approve(int id, CancellationToken cancellationToken)
    {
        var approved = await _adminOwnerRequestService.ApproveAsync(id, cancellationToken);
        return approved ? NoContent() : NotFound();
    }

    [HttpPost("{id:int}/reject")]
    public async Task<IActionResult> Reject(int id, CancellationToken cancellationToken)
    {
        var rejected = await _adminOwnerRequestService.RejectAsync(id, cancellationToken);
        return rejected ? NoContent() : NotFound();
    }
}
