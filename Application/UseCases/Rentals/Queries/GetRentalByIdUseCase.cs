using Application.DTOs.Rentals;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals;

public class GetRentalByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetRentalByIdUseCase> _logger;

    public GetRentalByIdUseCase(
        IUnitOfWork unitOfWork,
        ILogger<GetRentalByIdUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<RentalDto> ExecuteAsync(Guid id)
    {
        _logger.LogInformation("Obteniendo alquiler con ID: {RentalId}", id);

        var rentalRepo = _unitOfWork.GetRepository<Rental>();
        
        var rentals = await rentalRepo.GetAsync(
            filter: r => r.Id == id,
            includeProperties: "Customer,Place,Place.Location,User"
        );
        
        var rental = rentals.FirstOrDefault();
        
        if (rental == null)
        {
            throw new KeyNotFoundException($"Alquiler con ID {id} no encontrado");
        }

        return new RentalDto
        {
            Id = rental.Id,
            CustomerId = rental.CustomerId,
            PlaceId = rental.PlaceId,
            UserId = rental.UserId,
            StartDate = rental.StartDate,
            EndDate = rental.EndDate,
            Amount = rental.Amount,
            Status = rental.Status,
            CreatedAt = rental.CreatedAt,
            UpdatedAt = rental.UpdatedAt,
            Customer = rental.Customer != null ? new CustomerDto
            {
                Id = rental.Customer.Id,
                FullName = rental.Customer.FullName,
                Dni = rental.Customer.Dni,
                Phone = rental.Customer.Phone,
                Email = rental.Customer.Email,
                CreatedAt = rental.Customer.CreatedAt,
                UpdatedAt = rental.Customer.UpdatedAt
            } : null,
            Place = rental.Place != null ? new PlaceDto
            {
                Id = rental.Place.Id,
                LocationId = rental.Place.LocationId,
                Name = rental.Place.Name,
                Area = rental.Place.Area,
                ImagenUrl = rental.Place.ImagenUrl,
                CreatedAt = rental.Place.CreatedAt,
                UpdatedAt = rental.Place.UpdatedAt,
                Location = rental.Place.Location != null ? new LocationDto
                {
                    Id = rental.Place.Location.Id,
                    Name = rental.Place.Location.Name,
                    Address = rental.Place.Location.Address,
                    Capacity = rental.Place.Location.Capacity,
                    Status = rental.Place.Location.Status,
                    CreatedAt = rental.Place.Location.CreatedAt,
                    UpdatedAt = rental.Place.Location.UpdatedAt
                } : null
            } : null,
            User = rental.User != null ? new RentalUserDto
            {
                Id = rental.User.Id,
                Name = rental.User.Name,
                Email = rental.User.Email
            } : null
        };
    }
}
