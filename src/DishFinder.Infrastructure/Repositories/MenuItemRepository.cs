using DishFinder.Application.Interfaces.Repositories;
using DishFinder.Domain.Entities;
using DishFinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DishFinder.Infrastructure.Repositories;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly ApplicationDbContext _db;
    public MenuItemRepository(ApplicationDbContext db) => _db = db;

    public async Task AddAsync(MenuItem item, CancellationToken cancellationToken = default)
    {
        await _db.MenuItems.AddAsync(item, cancellationToken);
    }

    public async Task<MenuItem?> GetByRestaurantDishAsync(int restaurantId, int dishId, CancellationToken cancellationToken = default)
        => await _db.MenuItems.FirstOrDefaultAsync(m => m.RestaurantId == restaurantId && m.DishId == dishId, cancellationToken);

    public async Task<IEnumerable<MenuItem>> GetByRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default)
        => await _db.MenuItems.Where(m => m.RestaurantId == restaurantId).ToListAsync(cancellationToken);

    public Task UpdateAsync(MenuItem item, CancellationToken cancellationToken = default)
    {
        _db.MenuItems.Update(item);
        return Task.CompletedTask;
    }
}
