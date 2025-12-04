using DishFinder.Application.Interfaces.Repositories;
using DishFinder.Domain.Entities;
using DishFinder.Domain.Enums;
using DishFinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DishFinder.Infrastructure.Repositories;

public class OwnerRequestRepository : IOwnerRequestRepository
{
    private readonly ApplicationDbContext _db;
    public OwnerRequestRepository(ApplicationDbContext db) => _db = db;

    public async Task AddAsync(OwnerRequest request, CancellationToken cancellationToken = default)
    {
        await _db.OwnerRequests.AddAsync(request, cancellationToken);
    }

    public async Task<IEnumerable<OwnerRequest>> GetByStatusAsync(OwnerRequestStatus status, CancellationToken cancellationToken = default)
        => await _db.OwnerRequests.Where(o => o.Status == status).ToListAsync(cancellationToken);

    public Task<OwnerRequest?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => _db.OwnerRequests.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

    public Task UpdateAsync(OwnerRequest request, CancellationToken cancellationToken = default)
    {
        _db.OwnerRequests.Update(request);
        return Task.CompletedTask;
    }
}
