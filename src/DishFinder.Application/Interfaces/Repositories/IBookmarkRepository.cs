using DishFinder.Domain.Entities;

namespace DishFinder.Application.Interfaces.Repositories;

public interface IBookmarkRepository
{
    Task<IEnumerable<Bookmark>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Bookmark?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Bookmark bookmark, CancellationToken cancellationToken = default);
    Task DeleteAsync(Bookmark bookmark, CancellationToken cancellationToken = default);
}
