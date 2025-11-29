using Application.DTOs.Rentals;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Places;

public class GetPlaceByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetPlaceByIdUseCase> _logger;

    public GetPlaceByIdUseCase(IUnitOfWork unitOfWork, ILogger<GetPlaceByIdUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<PlaceDto> ExecuteAsync(Guid id)
    {
        var placeRepo = _unitOfWork.GetRepository<Place>();
        var places = await placeRepo.GetAsync(
            filter: p => p.Id == id,
            includeProperties: "Location");
        
        var place = places.FirstOrDefault();
        
        if (place == null)
            throw new KeyNotFoundException($"Lugar con ID {id} no encontrado");

        return new PlaceDto
        {
            Id = place.Id,
            LocationId = place.LocationId,
            Name = place.Name,
            Area = place.Area,
            ImagenUrl = place.ImagenUrl,
            CreatedAt = place.CreatedAt,
            UpdatedAt = place.UpdatedAt,
            Location = place.Location != null ? new LocationDto
            {
                Id = place.Location.Id,
                Name = place.Location.Name,
                Address = place.Location.Address,
                Capacity = place.Location.Capacity,
                Status = place.Location.Status,
                CreatedAt = place.Location.CreatedAt,
                UpdatedAt = place.Location.UpdatedAt
            } : null
        };
    }
}
