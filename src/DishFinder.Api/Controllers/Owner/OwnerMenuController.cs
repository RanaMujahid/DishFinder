using DishFinder.Application.DTOs.Restaurants;
using DishFinder.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DishFinder.Api.Controllers.Owner;

[ApiController]
[Route("owner/menu")]
[Authorize(Roles = "Owner")]
public class OwnerMenuController : ControllerBase
{
    private readonly IOwnerMenuService _ownerMenuService;

    public OwnerMenuController(IOwnerMenuService ownerMenuService)
    {
        _ownerMenuService = ownerMenuService;
    }

    [HttpGet("{restaurantId:int}")]
    public async Task<IActionResult> GetMenu(int restaurantId, CancellationToken cancellationToken)
    {
        var ownerId = Guid.Parse(User.FindFirst("sub")!.Value);
        try
        {
            var menu = await _ownerMenuService.GetMenuAsync(restaurantId, ownerId, cancellationToken);
            return Ok(menu);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMenuItemDto dto, CancellationToken cancellationToken)
    {
        var ownerId = Guid.Parse(User.FindFirst("sub")!.Value);
        try
        {
            var created = await _ownerMenuService.CreateAsync(ownerId, dto, cancellationToken);
            return Ok(created);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateMenuItemDto dto, CancellationToken cancellationToken)
    {
        var ownerId = Guid.Parse(User.FindFirst("sub")!.Value);
        try
        {
            var updated = await _ownerMenuService.UpdateAsync(id, ownerId, dto, cancellationToken);
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

    [HttpPut("{id:int}/toggle-availability")]
    public async Task<IActionResult> ToggleAvailability(int id, CancellationToken cancellationToken)
    {
        var ownerId = Guid.Parse(User.FindFirst("sub")!.Value);
        try
        {
            var updated = await _ownerMenuService.ToggleAvailabilityAsync(id, ownerId, cancellationToken);
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
