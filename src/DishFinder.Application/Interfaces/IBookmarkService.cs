using DishFinder.Application.DTOs.Bookmarks;

namespace DishFinder.Application.Interfaces;

public interface IBookmarkService
{
    Task<IEnumerable<BookmarkDto>> GetAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<BookmarkDto> CreateAsync(Guid userId, int? restaurantId, int? dishId, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid userId, int id, CancellationToken cancellationToken = default);
}
