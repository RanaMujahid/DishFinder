using AutoMapper;
using DishFinder.Application.DTOs.OwnerRequests;
using DishFinder.Application.Interfaces;
using DishFinder.Application.Interfaces.Repositories;
using DishFinder.Domain.Entities;
using DishFinder.Domain.Enums;

namespace DishFinder.Application.Services;

public class OwnerRequestService : IOwnerRequestService
{
    private readonly IOwnerRequestRepository _ownerRequestRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OwnerRequestService(IOwnerRequestRepository ownerRequestRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _ownerRequestRepository = ownerRequestRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OwnerRequestDto> CreateAsync(Guid userId, int restaurantId, CancellationToken cancellationToken = default)
    {
        var request = new OwnerRequest
        {
            UserId = userId,
            RestaurantId = restaurantId,
            Status = OwnerRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await _ownerRequestRepository.AddAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<OwnerRequestDto>(request);
    }

    public async Task<IEnumerable<OwnerRequestDto>> GetPendingAsync(CancellationToken cancellationToken = default)
    {
        //var requests = await _ownerRequestRepository.GetPendingAsync(cancellationToken);
        //return requests.Select(_mapper.Map<OwnerRequestDto>);
        return null; //TODO: need to fix this.
    }

    public async Task<bool> ApproveAsync(int id, CancellationToken cancellationToken = default)
    {
        var request = await _ownerRequestRepository.GetByIdAsync(id, cancellationToken);
        if (request == null)
        {
            return false;
        }

        request.Status = OwnerRequestStatus.Approved;
        await _ownerRequestRepository.UpdateAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> RejectAsync(int id, CancellationToken cancellationToken = default)
    {
        var request = await _ownerRequestRepository.GetByIdAsync(id, cancellationToken);
        if (request == null)
        {
            return false;
        }

        request.Status = OwnerRequestStatus.Rejected;
        await _ownerRequestRepository.UpdateAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
