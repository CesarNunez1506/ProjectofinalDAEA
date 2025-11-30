using Application.DTOs.Rentals;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Places;

public class GetAllPlacesUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllPlacesUseCase> _logger;

    public GetAllPlacesUseCase(IUnitOfWork unitOfWork, ILogger<GetAllPlacesUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<PlaceDto>> ExecuteAsync()
    {
        var placeRepo = _unitOfWork.GetRepository<Place>();
        var places = await placeRepo.GetAsync(includeProperties: "Location");

        return places.Select(p => new PlaceDto
        {
            Id = p.Id,
            LocationId = p.LocationId,
            Name = p.Name,
            Area = p.Area,
            ImagenUrl = p.ImagenUrl,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,
            Location = p.Location != null ? new LocationDto
            {
                Id = p.Location.Id,
                Name = p.Location.Name,
                Address = p.Location.Address,
                Capacity = p.Location.Capacity,
                Status = p.Location.Status,
                CreatedAt = p.Location.CreatedAt,
                UpdatedAt = p.Location.UpdatedAt
            } : null
        });
    }
}
