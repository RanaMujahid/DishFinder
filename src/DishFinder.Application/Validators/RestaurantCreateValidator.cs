using DishFinder.Application.DTOs.Restaurants;
using FluentValidation;

namespace DishFinder.Application.Validators;

public class RestaurantCreateValidator : AbstractValidator<RestaurantCreateDto>
{
    public RestaurantCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.AreaId).GreaterThan(0);
    }
}
