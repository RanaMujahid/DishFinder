using DishFinder.Application.DTOs.Restaurants;
using DishFinder.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DishFinder.Api.Controllers.Owner;

[ApiController]
[Route("owner/restaurants")]
[Authorize(Roles = "Owner")]
public class OwnerRestaurantController : ControllerBase
{
    private readonly IOwnerRestaurantService _ownerRestaurantService;

    public OwnerRestaurantController(IOwnerRestaurantService ownerRestaurantService)
    {
        _ownerRestaurantService = ownerRestaurantService;
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyRestaurants(CancellationToken cancellationToken)
    {
        var ownerId = Guid.Parse(User.FindFirst("sub")!.Value);
        var restaurants = await _ownerRestaurantService.GetMyRestaurantsAsync(ownerId, cancellationToken);
        return Ok(restaurants);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, EditRestaurantDto dto, CancellationToken cancellationToken)
    {
        var ownerId = Guid.Parse(User.FindFirst("sub")!.Value);
        try
        {
            var updated = await _ownerRestaurantService.UpdateAsync(id, ownerId, dto, cancellationToken);
            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }
}
