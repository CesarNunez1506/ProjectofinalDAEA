using Application.DTOs.Rentals;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Locations;

public class GetAllLocationsUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllLocationsUseCase> _logger;

    public GetAllLocationsUseCase(IUnitOfWork unitOfWork, ILogger<GetAllLocationsUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<LocationDto>> ExecuteAsync()
    {
        var locationRepo = _unitOfWork.GetRepository<Location>();
        var locations = await locationRepo.GetAsync();

        return locations.Select(l => new LocationDto
        {
            Id = l.Id,
            Name = l.Name,
            Address = l.Address,
            Capacity = l.Capacity,
            Status = l.Status,
            CreatedAt = l.CreatedAt,
            UpdatedAt = l.UpdatedAt
        });
    }
}
