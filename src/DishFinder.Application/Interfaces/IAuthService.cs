using DishFinder.Application.DTOs.Auth;

namespace DishFinder.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterUserDto dto, CancellationToken cancellationToken = default);
    Task<AuthResponseDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);
    Task<AuthResponseDto> RefreshAsync(RefreshTokenRequestDto dto, CancellationToken cancellationToken = default);
    Task<UserInfoDto?> GetCurrentUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
