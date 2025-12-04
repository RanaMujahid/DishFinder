using DishFinder.Application.Interfaces.Repositories;
using DishFinder.Domain.Entities;
using DishFinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DishFinder.Infrastructure.Repositories;

public class BookmarkRepository : IBookmarkRepository
{
    private readonly ApplicationDbContext _db;
    public BookmarkRepository(ApplicationDbContext db) => _db = db;

    public async Task AddAsync(Bookmark bookmark, CancellationToken cancellationToken = default)
    {
        await _db.Bookmarks.AddAsync(bookmark, cancellationToken);
    }

    public Task DeleteAsync(Bookmark bookmark, CancellationToken cancellationToken = default)
    {
        _db.Bookmarks.Remove(bookmark);
        return Task.CompletedTask;
    }

    public Task<Bookmark?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => _db.Bookmarks.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public async Task<IEnumerable<Bookmark>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _db.Bookmarks.Where(b => b.UserId == userId).ToListAsync(cancellationToken);
}
