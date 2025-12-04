using DishFinder.Domain.Entities;
using DishFinder.Domain.Enums;

namespace DishFinder.Application.Interfaces.Repositories;

public interface IOwnerRequestRepository
{
    Task AddAsync(OwnerRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<OwnerRequest>> GetByStatusAsync(OwnerRequestStatus status, CancellationToken cancellationToken = default);
    Task<OwnerRequest?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(OwnerRequest request, CancellationToken cancellationToken = default);
}
