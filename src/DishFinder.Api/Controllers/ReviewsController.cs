using DishFinder.Application.DTOs.Reviews;
using DishFinder.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DishFinder.Api.Controllers;

[ApiController]
[Route("reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("/dishes/{dishId:int}/reviews")]
    public async Task<IActionResult> GetForDish(int dishId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var reviews = await _reviewService.GetForDishAsync(dishId, page, pageSize, cancellationToken);
        return Ok(reviews);
    }

    [HttpPost("/dishes/{dishId:int}/reviews")]
    [Authorize]
    public async Task<ActionResult<ReviewDto>> Create(int dishId, ReviewCreateDto dto, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst("sub")!.Value);
        dto.DishId = dishId;
        var review = await _reviewService.CreateAsync(userId, dto, cancellationToken);
        return CreatedAtAction(nameof(GetForDish), new { dishId, page = 1, pageSize = 10 }, review);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<ActionResult<ReviewDto>> Update(int id, ReviewCreateDto dto, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst("sub")!.Value);
        var updated = await _reviewService.UpdateAsync(id, userId, dto, cancellationToken);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst("sub")!.Value);
        var deleted = await _reviewService.DeleteAsync(id, userId, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
