using Domain.Interfaces.Repositories.Rentals;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Rentals;

public class ToggleRentalStatusUseCase
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ToggleRentalStatusUseCase> _logger;

    public ToggleRentalStatusUseCase(
        IRentalRepository rentalRepository,
        IUnitOfWork unitOfWork,
        ILogger<ToggleRentalStatusUseCase> logger)
    {
        _rentalRepository = rentalRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        _logger.LogInformation("Cambiando estado del alquiler con ID: {RentalId}", id);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var rental = await _rentalRepository.ToggleStatusAsync(id);
            
            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("Estado del alquiler cambiado a: {Status}", rental.Status);

            return rental.Status;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cambiar estado del alquiler");
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
