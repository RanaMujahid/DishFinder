using DishFinder.Domain.Entities;

namespace DishFinder.Application.Interfaces;

public interface ITokenService
{
    (string accessToken, DateTime expires) CreateAccessToken(User user);
    string CreateRefreshToken();
}
