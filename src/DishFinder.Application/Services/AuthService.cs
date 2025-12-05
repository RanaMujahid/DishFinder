using AutoMapper;
using DishFinder.Application.DTOs.Auth;
using DishFinder.Application.Interfaces;
using DishFinder.Application.Interfaces.Repositories;
using DishFinder.Domain.Entities;
using DishFinder.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace DishFinder.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _users;
    private readonly ITokenService _tokens;
    private readonly IPasswordHasher<User> _hasher;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IUserRepository users, ITokenService tokens, IPasswordHasher<User> hasher, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _users = users;
        _tokens = tokens;
        _hasher = hasher;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterUserDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _users.GetByEmailAsync(dto.Email, cancellationToken);
            if (existing != null)
            {
                throw new InvalidOperationException("Email already registered");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Role = UserRole.User,
                CreatedAt = DateTime.UtcNow
            };
            user.PasswordHash = _hasher.HashPassword(user, dto.Password);

            await _users.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await GenerateTokensAsync(user, cancellationToken);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _users.GetByEmailAsync(dto.Email, cancellationToken) ?? throw new InvalidOperationException("Invalid credentials");
        var verification = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (verification == PasswordVerificationResult.Failed)
        {
            throw new InvalidOperationException("Invalid credentials");
        }

        return await GenerateTokensAsync(user, cancellationToken);
    }

    public async Task<AuthResponseDto> RefreshAsync(RefreshTokenRequestDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _users.GetByRefreshTokenAsync(dto.RefreshToken, cancellationToken) ?? throw new InvalidOperationException("Invalid refresh token");
        if (user.RefreshTokenExpiry == null || user.RefreshTokenExpiry < DateTime.UtcNow)
        {
            throw new InvalidOperationException("Refresh token expired");
        }

        return await GenerateTokensAsync(user, cancellationToken);
    }

    public async Task<UserInfoDto?> GetCurrentUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _users.GetByIdAsync(userId, cancellationToken);
        return user == null ? null : _mapper.Map<UserInfoDto>(user);
    }

    private async Task<AuthResponseDto> GenerateTokensAsync(User user, CancellationToken cancellationToken)
    {
        var (accessToken, expires) = _tokens.CreateAccessToken(user);
        var refreshToken = _tokens.CreateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        await _users.UpdateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = expires,
            User = _mapper.Map<UserInfoDto>(user)
        };
    }
}
