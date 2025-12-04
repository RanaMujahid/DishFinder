using DishFinder.Application.Interfaces.Repositories;
using DishFinder.Domain.Entities;
using DishFinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DishFinder.Infrastructure.Repositories;

public class DishRepository : IDishRepository
{
    private readonly ApplicationDbContext _db;
    public DishRepository(ApplicationDbContext db) => _db = db;

    public async Task<IEnumerable<Dish>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _db.Dishes.ToListAsync(cancellationToken);

    public Task<Dish?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => _db.Dishes.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
}
