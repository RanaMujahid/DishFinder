using DishFinder.Application.DTOs.Photos;

namespace DishFinder.Application.Interfaces;

public interface IAdminPhotoService
{
    Task<IEnumerable<PendingPhotoDto>> GetPendingAsync(CancellationToken cancellationToken = default);
    Task<bool> ApproveAsync(int photoId, CancellationToken cancellationToken = default);
    Task<bool> RejectAsync(int photoId, CancellationToken cancellationToken = default);
}
