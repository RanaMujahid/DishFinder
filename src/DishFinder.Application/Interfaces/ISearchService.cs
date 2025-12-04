using DishFinder.Application.Common;
using DishFinder.Application.DTOs.Search;

namespace DishFinder.Application.Interfaces;

public interface ISearchService
{
    Task<PagedResult<SearchResultDto>> SearchAsync(string dish, string area, int page, int pageSize, double? lat = null, double? lng = null, CancellationToken cancellationToken = default);
}
