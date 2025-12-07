using DishFinder.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DishFinder.Api.Controllers.Owner;

[ApiController]
[Route("owner/reviews")]
[Authorize(Roles = "Owner")]
public class OwnerReviewsController : ControllerBase
{
    private readonly IOwnerReviewService _ownerReviewService;

    public OwnerReviewsController(IOwnerReviewService ownerReviewService)
    {
        _ownerReviewService = ownerReviewService;
    }

    [HttpGet]
    public async Task<IActionResult> GetReviews(CancellationToken cancellationToken)
    {
        var ownerId = Guid.Parse(User.FindFirst("sub")!.Value);
        var reviews = await _ownerReviewService.GetForOwnerAsync(ownerId, cancellationToken);
        return Ok(reviews);
    }
}
