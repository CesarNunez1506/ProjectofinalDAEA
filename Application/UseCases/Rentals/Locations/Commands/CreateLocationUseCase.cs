using Application.DTOs.Rentals;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Locations;

public class CreateLocationUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateLocationUseCase> _logger;

    public CreateLocationUseCase(IUnitOfWork unitOfWork, ILogger<CreateLocationUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<LocationDto> ExecuteAsync(CreateLocationDto dto)
    {
        var location = new Location
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Address = dto.Address,
            Capacity = dto.Capacity,
            Status = dto.Status,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var locationRepo = _unitOfWork.GetRepository<Location>();
        await locationRepo.AddAsync(location);
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
