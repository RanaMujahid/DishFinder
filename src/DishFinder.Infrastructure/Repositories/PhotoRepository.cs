using DishFinder.Application.Interfaces.Repositories;
using DishFinder.Domain.Entities;
using DishFinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DishFinder.Infrastructure.Repositories;

public class PhotoRepository : IPhotoRepository
{
    private readonly ApplicationDbContext _db;
    public PhotoRepository(ApplicationDbContext db) => _db = db;

    public async Task AddAsync(Photo photo, CancellationToken cancellationToken = default)
    {
        await _db.Photos.AddAsync(photo, cancellationToken);
    }

    public Task<Photo?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => _db.Photos.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<IEnumerable<Photo>> GetForDishAsync(int dishId, CancellationToken cancellationToken = default)
        => await _db.Photos.Where(p => p.DishId == dishId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<Photo>> GetForRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default)
        => await _db.Photos.Where(p => p.RestaurantId == restaurantId).ToListAsync(cancellationToken);

    public Task UpdateAsync(Photo photo, CancellationToken cancellationToken = default)
    {
        _db.Photos.Update(photo);
        return Task.CompletedTask;
    }
}
