using DishFinder.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DishFinder.Api.Controllers;

[ApiController]
[Route("bookmarks")]
[Authorize]
public class BookmarksController : ControllerBase
{
    private readonly IBookmarkService _bookmarkService;

    public BookmarksController(IBookmarkService bookmarkService)
    {
        _bookmarkService = bookmarkService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst("sub")!.Value);
        var bookmarks = await _bookmarkService.GetAsync(userId, cancellationToken);
        return Ok(bookmarks);
    }

    public class BookmarkCreateRequest
    {
        public int? RestaurantId { get; set; }
        public int? DishId { get; set; }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookmarkCreateRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst("sub")!.Value);
        var bookmark = await _bookmarkService.CreateAsync(userId, request.RestaurantId, request.DishId, cancellationToken);
        return Ok(bookmark);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst("sub")!.Value);
        var deleted = await _bookmarkService.DeleteAsync(userId, id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
