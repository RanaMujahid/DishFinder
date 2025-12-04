using DishFinder.Application.DTOs.Auth;
using DishFinder.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DishFinder.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterUserDto dto, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(dto, cancellationToken);
        return Ok(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(dto, cancellationToken);
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponseDto>> Refresh(RefreshTokenRequestDto dto, CancellationToken cancellationToken)
    {
        var result = await _authService.RefreshAsync(dto, cancellationToken);
        return Ok(result);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserInfoDto>> Me(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst("sub")!.Value);
        var user = await _authService.GetCurrentUserAsync(userId, cancellationToken);
        return user == null ? NotFound() : Ok(user);
    }
}
