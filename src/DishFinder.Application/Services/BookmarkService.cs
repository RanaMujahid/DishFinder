using AutoMapper;
using DishFinder.Application.DTOs.Bookmarks;
using DishFinder.Application.Interfaces;
using DishFinder.Application.Interfaces.Repositories;
using DishFinder.Domain.Entities;

namespace DishFinder.Application.Services;

public class BookmarkService : IBookmarkService
{
    private readonly IBookmarkRepository _bookmarkRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BookmarkService(IBookmarkRepository bookmarkRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _bookmarkRepository = bookmarkRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookmarkDto>> GetAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var bookmarks = await _bookmarkRepository.GetByUserAsync(userId, cancellationToken);
        return bookmarks.Select(_mapper.Map<BookmarkDto>);
    }

    public async Task<BookmarkDto> CreateAsync(Guid userId, int? restaurantId, int? dishId, CancellationToken cancellationToken = default)
    {
        var bookmark = new Bookmark
        {
            UserId = userId,
            RestaurantId = restaurantId,
            DishId = dishId,
            CreatedAt = DateTime.UtcNow
        };

        await _bookmarkRepository.AddAsync(bookmark, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<BookmarkDto>(bookmark);
    }

    public async Task<bool> DeleteAsync(Guid userId, int id, CancellationToken cancellationToken = default)
    {
        var bookmark = await _bookmarkRepository.GetByIdAsync(id, cancellationToken);
        if (bookmark == null || bookmark.UserId != userId)
        {
            return false;
        }

        await _bookmarkRepository.DeleteAsync(bookmark, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
