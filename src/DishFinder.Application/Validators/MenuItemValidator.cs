using DishFinder.Application.DTOs.Restaurants;
using FluentValidation;

namespace DishFinder.Application.Validators;

public class MenuItemValidator : AbstractValidator<MenuItemDto>
{
    public MenuItemValidator()
    {
        RuleFor(x => x.DishId).GreaterThan(0);
        RuleFor(x => x.Price).GreaterThan(0);
    }
}
