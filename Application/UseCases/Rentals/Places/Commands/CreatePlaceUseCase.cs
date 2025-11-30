using Application.DTOs.Rentals;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Places;

public class CreatePlaceUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreatePlaceUseCase> _logger;

    public CreatePlaceUseCase(IUnitOfWork unitOfWork, ILogger<CreatePlaceUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<PlaceDto> ExecuteAsync(CreatePlaceDto dto)
    {
        var locationRepo = _unitOfWork.GetRepository<Location>();
        var location = await locationRepo.GetByIdAsync(dto.LocationId);
        
        if (location == null)
            throw new KeyNotFoundException($"Ubicación con ID {dto.LocationId} no encontrada");

        var place = new Place
        {
            Id = Guid.NewGuid(),
            LocationId = dto.LocationId,
            Name = dto.Name,
            Area = dto.Area,
            ImagenUrl = dto.ImagenUrl,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var placeRepo = _unitOfWork.GetRepository<Place>();
        await placeRepo.AddAsync(place);
        await _unitOfWork.SaveChangesAsync();

        return new PlaceDto
        {
            Id = place.Id,
            LocationId = place.LocationId,
            Name = place.Name,
            Area = place.Area,
            ImagenUrl = place.ImagenUrl,
            CreatedAt = place.CreatedAt,
            UpdatedAt = place.UpdatedAt
        };
    }
}
