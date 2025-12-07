using DishFinder.Application.DTOs.Photos;
using DishFinder.Application.Interfaces;
using DishFinder.Application.Interfaces.Repositories;

namespace DishFinder.Application.Services;

public class AdminPhotoService : IAdminPhotoService
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdminPhotoService(IPhotoRepository photoRepository, IUnitOfWork unitOfWork)
    {
        _photoRepository = photoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PendingPhotoDto>> GetPendingAsync(CancellationToken cancellationToken = default)
    {
        var photos = await _photoRepository.GetPendingAsync(cancellationToken);
        return photos.Select(p => new PendingPhotoDto
        {
            Id = p.Id,
            Url = p.Url,
            RestaurantId = p.RestaurantId,
            RestaurantName = p.Restaurant?.Name,
            DishId = p.DishId,
            DishName = p.Dish?.Name,
            ReviewId = p.ReviewId,
            IsApproved = p.IsApproved,
            CreatedAt = p.CreatedAt
        });
    }

    public async Task<bool> ApproveAsync(int photoId, CancellationToken cancellationToken = default)
    {
        var photo = await _photoRepository.GetByIdAsync(photoId, cancellationToken);
        if (photo == null)
        {
            return false;
        }

        photo.IsApproved = true;
        await _photoRepository.UpdateAsync(photo, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> RejectAsync(int photoId, CancellationToken cancellationToken = default)
    {
        var photo = await _photoRepository.GetByIdAsync(photoId, cancellationToken);
        if (photo == null)
        {
            return false;
        }

        await _photoRepository.DeleteAsync(photo, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
