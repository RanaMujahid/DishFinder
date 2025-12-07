using DishFinder.Application.DTOs.OwnerRequests;
using DishFinder.Application.Interfaces;
using DishFinder.Application.Interfaces.Repositories;
using DishFinder.Domain.Enums;

namespace DishFinder.Application.Services;

public class AdminOwnerRequestService : IAdminOwnerRequestService
{
    private readonly IOwnerRequestRepository _ownerRequestRepository;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdminOwnerRequestService(
        IOwnerRequestRepository ownerRequestRepository,
        IRestaurantRepository restaurantRepository,
        IUnitOfWork unitOfWork)
    {
        _ownerRequestRepository = ownerRequestRepository;
        _restaurantRepository = restaurantRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OwnerRequestDto>> GetPendingAsync(CancellationToken cancellationToken = default)
    {
        var requests = await _ownerRequestRepository.GetByStatusAsync(OwnerRequestStatus.Pending, cancellationToken);
        return requests.Select(r => new OwnerRequestDto
        {
            Id = r.Id,
            RestaurantId = r.RestaurantId,
            UserId = r.UserId,
            Status = r.Status,
            CreatedAt = r.CreatedAt
        });
    }

    public async Task<bool> ApproveAsync(int requestId, CancellationToken cancellationToken = default)
    {
        var request = await _ownerRequestRepository.GetByIdAsync(requestId, cancellationToken);
        if (request == null || request.Status != OwnerRequestStatus.Pending)
        {
            return false;
        }

        var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId, cancellationToken);
        if (restaurant == null)
        {
            return false;
        }

        restaurant.OwnerUserId = request.UserId;
        request.Status = OwnerRequestStatus.Approved;

        await _restaurantRepository.UpdateAsync(restaurant, cancellationToken);
        await _ownerRequestRepository.UpdateAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> RejectAsync(int requestId, CancellationToken cancellationToken = default)
    {
        var request = await _ownerRequestRepository.GetByIdAsync(requestId, cancellationToken);
        if (request == null || request.Status != OwnerRequestStatus.Pending)
        {
            return false;
        }

        request.Status = OwnerRequestStatus.Rejected;
        await _ownerRequestRepository.UpdateAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
