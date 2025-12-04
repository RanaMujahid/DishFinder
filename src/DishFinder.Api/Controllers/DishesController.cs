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
