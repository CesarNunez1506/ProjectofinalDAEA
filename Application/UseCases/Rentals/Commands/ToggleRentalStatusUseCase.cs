using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals;

public class ToggleRentalStatusUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ToggleRentalStatusUseCase> _logger;

    public ToggleRentalStatusUseCase(
        IUnitOfWork unitOfWork,
        ILogger<ToggleRentalStatusUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        _logger.LogInformation("Cambiando estado del alquiler con ID: {RentalId}", id);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var rentalRepo = _unitOfWork.GetRepository<Rental>();
            var rental = await rentalRepo.GetByIdAsync(id);
            
            if (rental == null)
            {
                throw new KeyNotFoundException($"Alquiler con ID {id} no encontrado");
            }

            rental.Status = !rental.Status;
            rental.UpdatedAt = DateTime.UtcNow;
            
            rentalRepo.Update(rental);
            await _unitOfWork.SaveChangesAsync();
            
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
