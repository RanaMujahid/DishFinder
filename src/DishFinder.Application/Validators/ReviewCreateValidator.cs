using DishFinder.Application.DTOs.Reviews;
using FluentValidation;

namespace DishFinder.Application.Validators;

public class ReviewCreateValidator : AbstractValidator<ReviewCreateDto>
{
    public ReviewCreateValidator()
    {
        RuleFor(x => x.RestaurantId).GreaterThan(0);
        RuleFor(x => x.DishId).GreaterThan(0);
        RuleFor(x => x.RatingTaste).InclusiveBetween(1,5);
        RuleFor(x => x.RatingPortion).InclusiveBetween(1,5);
        RuleFor(x => x.RatingValue).InclusiveBetween(1,5);
    }
}
