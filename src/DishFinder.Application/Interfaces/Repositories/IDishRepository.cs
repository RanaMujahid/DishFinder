using DishFinder.Domain.Entities;

namespace DishFinder.Application.Interfaces.Repositories;

public interface IDishRepository
{
    Task<Dish?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Dish>> GetAllAsync(CancellationToken cancellationToken = default);
}
