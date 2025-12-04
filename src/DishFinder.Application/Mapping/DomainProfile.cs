using AutoMapper;
using DishFinder.Application.DTOs.Auth;
using DishFinder.Application.DTOs.Bookmarks;
using DishFinder.Application.DTOs.OwnerRequests;
using DishFinder.Application.DTOs.Photos;
using DishFinder.Application.DTOs.Restaurants;
using DishFinder.Application.DTOs.Reviews;
using DishFinder.Domain.Entities;

namespace DishFinder.Application.Mapping;

public class DomainProfile : Profile
{
    public DomainProfile()
    {
        CreateMap<User, UserInfoDto>();
        CreateMap<Restaurant, RestaurantDto>().ReverseMap();
        CreateMap<RestaurantCreateDto, Restaurant>();
        CreateMap<MenuItem, MenuItemDto>().ReverseMap();
        CreateMap<Review, ReviewDto>();
        CreateMap<ReviewCreateDto, Review>();
        CreateMap<Photo, PhotoDto>();
        CreateMap<Bookmark, BookmarkDto>();
        CreateMap<OwnerRequest, OwnerRequestDto>();
    }
}
