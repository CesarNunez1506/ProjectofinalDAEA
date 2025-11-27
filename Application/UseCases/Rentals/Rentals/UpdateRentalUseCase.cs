using Application.DTOs.Rentals;
using Domain.Interfaces.Repositories.Rentals;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Rentals;

public class UpdateRentalUseCase
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateRentalUseCase> _logger;

    public UpdateRentalUseCase(
        IRentalRepository rentalRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateRentalUseCase> logger)
    {
        _rentalRepository = rentalRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<RentalDto> ExecuteAsync(Guid id, UpdateRentalDto dto)
    {
        _logger.LogInformation("Actualizando alquiler con ID: {RentalId}", id);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var rental = await _rentalRepository.GetByIdAsync(id);
            if (rental == null)
            {
                throw new KeyNotFoundException($"Alquiler con ID {id} no encontrado");
            }

            if (dto.CustomerId.HasValue)
                rental.CustomerId = dto.CustomerId.Value;

            if (dto.PlaceId.HasValue)
                rental.PlaceId = dto.PlaceId.Value;

            if (dto.UserId.HasValue)
                rental.UserId = dto.UserId.Value;

            if (dto.StartDate.HasValue)
                rental.StartDate = dto.StartDate.Value;

            if (dto.EndDate.HasValue)
                rental.EndDate = dto.EndDate.Value;

            if (dto.Amount.HasValue)
                rental.Amount = dto.Amount.Value;

            if (dto.Status.HasValue)
                rental.Status = dto.Status.Value;

            if (dto.StartDate.HasValue || dto.EndDate.HasValue)
            {
                if (rental.StartDate >= rental.EndDate)
                {
                    throw new ArgumentException("La fecha de inicio debe ser anterior a la fecha de fin");
                }

                var overlap = await _rentalRepository.CheckOverlapAsync(
                    rental.PlaceId, 
                    rental.StartDate, 
                    rental.EndDate, 
                    id);

                if (overlap != null)
                {
                    throw new InvalidOperationException(
                        $"No se puede actualizar el alquiler: existe un solapamiento con otro alquiler activo en las fechas indicadas");
                }
            }

            var updatedRental = await _rentalRepository.UpdateAsync(rental);

            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("Alquiler actualizado exitosamente");

            var rentalWithRelations = await _rentalRepository.GetByIdWithRelationsAsync(id);

            return new RentalDto
            {
                Id = rentalWithRelations!.Id,
                CustomerId = rentalWithRelations.CustomerId,
                PlaceId = rentalWithRelations.PlaceId,
                UserId = rentalWithRelations.UserId,
                StartDate = rentalWithRelations.StartDate,
                EndDate = rentalWithRelations.EndDate,
                Amount = rentalWithRelations.Amount,
                Status = rentalWithRelations.Status,
                CreatedAt = rentalWithRelations.CreatedAt,
                UpdatedAt = rentalWithRelations.UpdatedAt
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar alquiler");
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
