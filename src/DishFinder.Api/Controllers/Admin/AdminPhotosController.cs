using DishFinder.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DishFinder.Api.Controllers.Admin;

[ApiController]
[Route("admin/photos")]
[Authorize(Roles = "Admin")]
public class AdminPhotosController : ControllerBase
{
    private readonly IAdminPhotoService _adminPhotoService;

    public AdminPhotosController(IAdminPhotoService adminPhotoService)
    {
        _adminPhotoService = adminPhotoService;
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPending(CancellationToken cancellationToken)
    {
        var photos = await _adminPhotoService.GetPendingAsync(cancellationToken);
        return Ok(photos);
    }

    [HttpPost("{id:int}/approve")]
    public async Task<IActionResult> Approve(int id, CancellationToken cancellationToken)
    {
        var approved = await _adminPhotoService.ApproveAsync(id, cancellationToken);
        return approved ? NoContent() : NotFound();
    }

    [HttpPost("{id:int}/reject")]
    public async Task<IActionResult> Reject(int id, CancellationToken cancellationToken)
    {
        var rejected = await _adminPhotoService.RejectAsync(id, cancellationToken);
        return rejected ? NoContent() : NotFound();
    }
}
