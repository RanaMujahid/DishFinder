using DishFinder.Application.DTOs.Restaurants;
using DishFinder.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DishFinder.Api.Controllers;

[ApiController]
[Route("restaurants")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RestaurantDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantService.GetByIdAsync(id, cancellationToken);
        return restaurant == null ? NotFound() : Ok(restaurant);
    }

    [HttpGet("{id:int}/menu")]
    public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetMenu(int id, CancellationToken cancellationToken)
    {
        var menu = await _restaurantService.GetMenuAsync(id, cancellationToken);
        return Ok(menu);
    }

    [HttpGet("by-area/{areaId:int}")]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetByArea(int areaId, CancellationToken cancellationToken)
    {
        var restaurants = await _restaurantService.GetByAreaAsync(areaId, cancellationToken);
        return Ok(restaurants);
    }

    [HttpPost]
    [Authorize(Roles = "Owner,Admin")]
    public async Task<ActionResult<RestaurantDto>> Create(RestaurantCreateDto dto, CancellationToken cancellationToken)
    {
        var ownerId = Guid.Parse(User.FindFirst("sub")!.Value);
        var created = await _restaurantService.CreateAsync(dto, ownerId, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Owner,Admin")]
    public async Task<ActionResult<RestaurantDto>> Update(int id, RestaurantCreateDto dto, CancellationToken cancellationToken)
    {
        var ownerId = Guid.Parse(User.FindFirst("sub")!.Value);
        var updated = await _restaurantService.UpdateAsync(id, dto, ownerId, cancellationToken);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _restaurantService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
