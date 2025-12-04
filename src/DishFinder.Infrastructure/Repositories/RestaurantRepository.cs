using DishFinder.Application.Interfaces.Repositories;
using DishFinder.Domain.Entities;
using DishFinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DishFinder.Infrastructure.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly ApplicationDbContext _db;
    public RestaurantRepository(ApplicationDbContext db) => _db = db;

    public async Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
    {
        await _db.Restaurants.AddAsync(restaurant, cancellationToken);
    }

    public Task DeleteAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
    {
        _db.Restaurants.Remove(restaurant);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Restaurant>> GetByAreaAsync(int areaId, CancellationToken cancellationToken = default)
    {
        return await _db.Restaurants.Where(r => r.AreaId == areaId).ToListAsync(cancellationToken);
    }

    public Task<Restaurant?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => _db.Restaurants.Include(r => r.MenuItems).FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public Task UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
    {
        _db.Restaurants.Update(restaurant);
        return Task.CompletedTask;
    }
}
