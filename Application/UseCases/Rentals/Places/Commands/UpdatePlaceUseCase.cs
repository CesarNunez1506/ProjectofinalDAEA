using Application.DTOs.Rentals;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Places;

public class UpdatePlaceUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdatePlaceUseCase> _logger;

    public UpdatePlaceUseCase(IUnitOfWork unitOfWork, ILogger<UpdatePlaceUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<PlaceDto> ExecuteAsync(Guid id, UpdatePlaceDto dto)
    {
        var placeRepo = _unitOfWork.GetRepository<Place>();
        var place = await placeRepo.GetByIdAsync(id);

        if (place == null)
            throw new KeyNotFoundException($"Lugar con ID {id} no encontrado");

        if (dto.LocationId.HasValue) place.LocationId = dto.LocationId.Value;
        if (dto.Name != null) place.Name = dto.Name;
        if (dto.Area != null) place.Area = dto.Area;
        if (dto.ImagenUrl != null) place.ImagenUrl = dto.ImagenUrl;
        
        place.UpdatedAt = DateTime.UtcNow;

        placeRepo.Update(place);
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
