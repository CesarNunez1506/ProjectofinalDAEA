using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Locations;

public class DeleteLocationUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteLocationUseCase> _logger;

    public DeleteLocationUseCase(IUnitOfWork unitOfWork, ILogger<DeleteLocationUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var locationRepo = _unitOfWork.GetRepository<Location>();
        var location = await locationRepo.GetByIdAsync(id);

        if (location == null)
            throw new KeyNotFoundException($"Ubicación con ID {id} no encontrada");

        locationRepo.Remove(location);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
