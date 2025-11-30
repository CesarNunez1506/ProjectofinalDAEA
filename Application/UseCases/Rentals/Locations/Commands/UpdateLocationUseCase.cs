using Application.DTOs.Rentals;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Locations;

public class UpdateLocationUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateLocationUseCase> _logger;

    public UpdateLocationUseCase(IUnitOfWork unitOfWork, ILogger<UpdateLocationUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<LocationDto> ExecuteAsync(Guid id, UpdateLocationDto dto)
    {
        var locationRepo = _unitOfWork.GetRepository<Location>();
        var location = await locationRepo.GetByIdAsync(id);

        if (location == null)
            throw new KeyNotFoundException($"Ubicación con ID {id} no encontrada");

        if (dto.Name != null) location.Name = dto.Name;
        if (dto.Address != null) location.Address = dto.Address;
        if (dto.Capacity.HasValue) location.Capacity = dto.Capacity.Value;
        if (dto.Status != null) location.Status = dto.Status;
        
        location.UpdatedAt = DateTime.UtcNow;

        locationRepo.Update(location);
        await _unitOfWork.SaveChangesAsync();

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
