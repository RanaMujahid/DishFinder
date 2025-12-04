using DishFinder.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DishFinder.Api.Controllers;

[ApiController]
[Route("photos")]
public class PhotosController : ControllerBase
{
    private readonly IPhotoService _photoService;
    private readonly IWebHostEnvironment _env;

    public PhotosController(IPhotoService photoService, IWebHostEnvironment env)
    {
        _photoService = photoService;
        _env = env;
    }

    [HttpPost("upload")]
    [Authorize]
    [RequestSizeLimit(10_000_000)]
    public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] int? restaurantId, [FromForm] int? dishId, [FromForm] int? reviewId, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File is required.");
        }

        var uploadsPath = Path.Combine(_env.ContentRootPath, "uploads", "photos");
        Directory.CreateDirectory(uploadsPath);
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadsPath, fileName);

        await using (var stream = System.IO.File.Create(filePath))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        var relativeUrl = $"/uploads/photos/{fileName}";
        var photo = await _photoService.UploadAsync(restaurantId, dishId, reviewId, relativeUrl, cancellationToken);
        return Ok(photo);
    }

    [HttpGet("restaurant/{id:int}")]
    public async Task<IActionResult> GetForRestaurant(int id, CancellationToken cancellationToken)
    {
        var photos = await _photoService.GetForRestaurantAsync(id, cancellationToken);
        return Ok(photos);
    }

    [HttpGet("dish/{id:int}")]
    public async Task<IActionResult> GetForDish(int id, CancellationToken cancellationToken)
    {
        var photos = await _photoService.GetForDishAsync(id, cancellationToken);
        return Ok(photos);
    }

    [HttpPut("{id:int}/approve")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Approve(int id, CancellationToken cancellationToken)
    {
        var updated = await _photoService.ModerateAsync(id, true, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpPut("{id:int}/reject")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Reject(int id, CancellationToken cancellationToken)
    {
        var updated = await _photoService.ModerateAsync(id, false, cancellationToken);
        return updated ? NoContent() : NotFound();
    }
}
