using DishFinder.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DishFinder.Api.Controllers;

[ApiController]
[Route("search")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] string dish, [FromQuery] string area, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] double? lat = null, [FromQuery] double? lng = null, CancellationToken cancellationToken = default)
    {
        var results = await _searchService.SearchAsync(dish, area, page, pageSize, lat, lng, cancellationToken);
        return Ok(results);
    }
}
