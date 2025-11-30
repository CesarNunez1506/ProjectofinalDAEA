using Application.DTOs.Rentals;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals;

public class UpdateRentalUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateRentalUseCase> _logger;

    public UpdateRentalUseCase(
        IUnitOfWork unitOfWork,
        ILogger<UpdateRentalUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<RentalDto> ExecuteAsync(Guid id, UpdateRentalDto dto)
    {
        _logger.LogInformation("Actualizando alquiler con ID: {RentalId}", id);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var rentalRepo = _unitOfWork.GetRepository<Rental>();
            var rental = await rentalRepo.GetByIdAsync(id);
            
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

                var overlaps = await rentalRepo.FindAsync(r => 
                    r.PlaceId == rental.PlaceId && 
                    r.Status == true &&
                    r.StartDate <= rental.EndDate && 
                    r.EndDate >= rental.StartDate &&
                    r.Id != id);
                
                var overlap = overlaps.FirstOrDefault();
                if (overlap != null)
                {
                    throw new InvalidOperationException(
                        $"No se puede actualizar el alquiler: existe un solapamiento con otro alquiler activo en las fechas indicadas");
                }
            }

            rental.UpdatedAt = DateTime.UtcNow;
            rentalRepo.Update(rental);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("Alquiler actualizado exitosamente");

            var rentalWithRelations = await rentalRepo.GetAsync(
                filter: r => r.Id == id,
                includeProperties: "Customer,Place,Place.Location,User");
            
            var updatedRental = rentalWithRelations.FirstOrDefault();

            return new RentalDto
            {
                Id = updatedRental!.Id,
                CustomerId = updatedRental.CustomerId,
                PlaceId = updatedRental.PlaceId,
                UserId = updatedRental.UserId,
                StartDate = updatedRental.StartDate,
                EndDate = updatedRental.EndDate,
                Amount = updatedRental.Amount,
                Status = updatedRental.Status,
                CreatedAt = updatedRental.CreatedAt,
                UpdatedAt = updatedRental.UpdatedAt,
                Customer = updatedRental.Customer != null ? new CustomerDto
                {
                    Id = updatedRental.Customer.Id,
                    FullName = updatedRental.Customer.FullName,
                    Dni = updatedRental.Customer.Dni,
                    Phone = updatedRental.Customer.Phone,
                    Email = updatedRental.Customer.Email,
                    CreatedAt = updatedRental.Customer.CreatedAt,
                    UpdatedAt = updatedRental.Customer.UpdatedAt
                } : null,
                Place = updatedRental.Place != null ? new PlaceDto
                {
                    Id = updatedRental.Place.Id,
                    LocationId = updatedRental.Place.LocationId,
                    Name = updatedRental.Place.Name,
                    Area = updatedRental.Place.Area,
                    ImagenUrl = updatedRental.Place.ImagenUrl,
                    CreatedAt = updatedRental.Place.CreatedAt,
                    UpdatedAt = updatedRental.Place.UpdatedAt,
                    Location = updatedRental.Place.Location != null ? new LocationDto
                    {
                        Id = updatedRental.Place.Location.Id,
                        Name = updatedRental.Place.Location.Name,
                        Address = updatedRental.Place.Location.Address,
                        Capacity = updatedRental.Place.Location.Capacity,
                        Status = updatedRental.Place.Location.Status,
                        CreatedAt = updatedRental.Place.Location.CreatedAt,
                        UpdatedAt = updatedRental.Place.Location.UpdatedAt
                    } : null
                } : null,
                User = updatedRental.User != null ? new RentalUserDto
                {
                    Id = updatedRental.User.Id,
                    Name = updatedRental.User.Name,
                    Email = updatedRental.User.Email
                } : null
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
