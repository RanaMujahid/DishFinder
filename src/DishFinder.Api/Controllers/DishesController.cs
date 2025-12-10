using DishFinder.Application.DTOs.Restaurants;
using DishFinder.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DishFinder.Api.Controllers;

[ApiController]
[Route("dishes")]
public class DishesController : ControllerBase
{
    private readonly IDishService _dishService;

    public DishesController(IDishService dishService)
    {
        _dishService = dishService;
    }

    [HttpGet("trending")]
    public async Task<IActionResult> Trending(CancellationToken cancellationToken)
    {
        var trending = new[]
        {
            new { name = "Biryani", count = "2.5k reviews", image = "https://images.unsplash.com/photo-1563379091339-03b21ab4a4f8?w=400" },
            new { name = "Shawarma", count = "3.2k reviews", image = "https://images.unsplash.com/photo-1529006557810-274b9b2fc783?w=400" },
            new { name = "Hummus", count = "1.8k reviews", image = "https://images.unsplash.com/photo-1577805947697-89e18249d767?w=400" },
            new { name = "Kebab", count = "2.1k reviews", image = "https://images.unsplash.com/photo-1544025162-d76694265947?w=400" },
            new { name = "Falafel", count = "1.5k reviews", image = "https://images.unsplash.com/photo-1565557623262-b51c2513a641?w=400" },
        };

        return Ok(trending);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DishDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dish = await _dishService.GetByIdAsync(id, cancellationToken);
        return dish == null ? NotFound() : Ok(dish);
    }

    [HttpGet("/restaurants/{restaurantId:int}/dishes/{dishId:int}")]
    public async Task<ActionResult<MenuItemDto>> GetRestaurantDish(int restaurantId, int dishId, CancellationToken cancellationToken)
    {
        var menuItem = await _dishService.GetRestaurantDishAsync(restaurantId, dishId, cancellationToken);
        return menuItem == null ? NotFound() : Ok(menuItem);
    }
}
