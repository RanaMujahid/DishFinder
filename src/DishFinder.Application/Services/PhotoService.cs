using AutoMapper;
using DishFinder.Application.DTOs.Photos;
using DishFinder.Application.Interfaces;
using DishFinder.Application.Interfaces.Repositories;
using DishFinder.Domain.Entities;

namespace DishFinder.Application.Services;

public class PhotoService : IPhotoService
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PhotoService(IPhotoRepository photoRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _photoRepository = photoRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PhotoDto> UploadAsync(int? restaurantId, int? dishId, int? reviewId, string url, CancellationToken cancellationToken = default)
    {
        var photo = new Photo
        {
            RestaurantId = restaurantId,
            DishId = dishId,
            ReviewId = reviewId,
            Url = url,
            CreatedAt = DateTime.UtcNow,
            IsApproved = false
        };

        await _photoRepository.AddAsync(photo, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PhotoDto>(photo);
    }

    public async Task<IEnumerable<PhotoDto>> GetForRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default)
    {
        var photos = await _photoRepository.GetForRestaurantAsync(restaurantId, cancellationToken);
        return photos.Select(_mapper.Map<PhotoDto>);
    }

    public async Task<IEnumerable<PhotoDto>> GetForDishAsync(int dishId, CancellationToken cancellationToken = default)
    {
        var photos = await _photoRepository.GetForDishAsync(dishId, cancellationToken);
        return photos.Select(_mapper.Map<PhotoDto>);
    }

    public async Task<bool> ModerateAsync(int id, bool approve, CancellationToken cancellationToken = default)
    {
        var photo = await _photoRepository.GetByIdAsync(id, cancellationToken);
        if (photo == null)
        {
            return false;
        }

        photo.IsApproved = approve;
        await _photoRepository.UpdateAsync(photo, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
