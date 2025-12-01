using Application.DTOs.Rentals;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Locations;

public class GetLocationByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetLocationByIdUseCase> _logger;

    public GetLocationByIdUseCase(IUnitOfWork unitOfWork, ILogger<GetLocationByIdUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<LocationDto> ExecuteAsync(Guid id)
    {
        var locationRepo = _unitOfWork.GetRepository<Location>();
        var location = await locationRepo.GetByIdAsync(id);
        
        if (location == null)
            throw new KeyNotFoundException($"Ubicación con ID {id} no encontrada");

        return new LocationDto
        {
            Id = location.Id,
            Name = location.Name,
            Address = location.Address,
            Capacity = location.Capacity,
            Status = location.Status,
            CreatedAt = location.CreatedAt,
            UpdatedAt = location.UpdatedAt
        };
    }
}
