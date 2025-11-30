using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Places;

public class DeletePlaceUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeletePlaceUseCase> _logger;

    public DeletePlaceUseCase(IUnitOfWork unitOfWork, ILogger<DeletePlaceUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var placeRepo = _unitOfWork.GetRepository<Place>();
        var place = await placeRepo.GetByIdAsync(id);

        if (place == null)
            throw new KeyNotFoundException($"Lugar con ID {id} no encontrado");

        placeRepo.Remove(place);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
