using Application.DTOs.Rentals;
using Domain.Interfaces.Repositories.Rentals;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Rentals;

public class GetAllRentalsUseCase
{
    private readonly IRentalRepository _rentalRepository;
    private readonly ILogger<GetAllRentalsUseCase> _logger;

    public GetAllRentalsUseCase(
        IRentalRepository rentalRepository,
        ILogger<GetAllRentalsUseCase> logger)
    {
        _rentalRepository = rentalRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<RentalDto>> ExecuteAsync()
    {
        _logger.LogInformation("Obteniendo todos los alquileres");

        var rentals = await _rentalRepository.GetAllWithRelationsAsync();

        return rentals.Select(r => new RentalDto
        {
            Id = r.Id,
            CustomerId = r.CustomerId,
            PlaceId = r.PlaceId,
            UserId = r.UserId,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            Amount = r.Amount,
            Status = r.Status,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt,
            Customer = r.Customer != null ? new CustomerDto
            {
                Id = r.Customer.Id,
                FullName = r.Customer.FullName,
                Dni = r.Customer.Dni,
                Phone = r.Customer.Phone,
                Email = r.Customer.Email,
                CreatedAt = r.Customer.CreatedAt,
                UpdatedAt = r.Customer.UpdatedAt
            } : null,
            Place = r.Place != null ? new PlaceDto
            {
                Id = r.Place.Id,
                LocationId = r.Place.LocationId,
                Name = r.Place.Name,
                Area = r.Place.Area,
                ImagenUrl = r.Place.ImagenUrl,
                CreatedAt = r.Place.CreatedAt,
                UpdatedAt = r.Place.UpdatedAt,
                Location = r.Place.Location != null ? new LocationDto
                {
                    Id = r.Place.Location.Id,
                    Name = r.Place.Location.Name,
                    Address = r.Place.Location.Address,
                    Capacity = r.Place.Location.Capacity,
                    Status = r.Place.Location.Status,
                    CreatedAt = r.Place.Location.CreatedAt,
                    UpdatedAt = r.Place.Location.UpdatedAt
                } : null
            } : null,
            User = r.User != null ? new UserDto
            {
                Id = r.User.Id,
                Name = r.User.Name,
                Email = r.User.Email
            } : null
        });
    }
}
