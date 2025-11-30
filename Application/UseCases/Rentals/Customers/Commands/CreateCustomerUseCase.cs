using Application.DTOs.Rentals;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Customers;

public class CreateCustomerUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCustomerUseCase> _logger;

    public CreateCustomerUseCase(
        IUnitOfWork unitOfWork,
        ILogger<CreateCustomerUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<CustomerDto> ExecuteAsync(CreateCustomerDto dto)
    {
        _logger.LogInformation("Creando cliente: {FullName}", dto.FullName);

        var customerRepo = _unitOfWork.GetRepository<Customer>();

        // Verificar si ya existe un cliente con el mismo DNI
        var existingCustomer = await customerRepo.FirstOrDefaultAsync(c => c.Dni == dto.Dni);
        if (existingCustomer != null)
        {
            throw new InvalidOperationException($"Ya existe un cliente con el DNI {dto.Dni}");
        }

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            Dni = dto.Dni,
            Phone = dto.Phone,
            Email = dto.Email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await customerRepo.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Cliente creado con ID: {CustomerId}", customer.Id);

        return new CustomerDto
        {
            Id = customer.Id,
            FullName = customer.FullName,
            Dni = customer.Dni,
            Phone = customer.Phone,
            Email = customer.Email,
            CreatedAt = customer.CreatedAt,
            UpdatedAt = customer.UpdatedAt
        };
    }
}
