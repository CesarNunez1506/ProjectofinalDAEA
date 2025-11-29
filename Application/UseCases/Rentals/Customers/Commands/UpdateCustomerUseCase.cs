using Application.DTOs.Rentals;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Customers;

public class UpdateCustomerUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateCustomerUseCase> _logger;

    public UpdateCustomerUseCase(IUnitOfWork unitOfWork, ILogger<UpdateCustomerUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<CustomerDto> ExecuteAsync(Guid id, UpdateCustomerDto dto)
    {
        var customerRepo = _unitOfWork.GetRepository<Customer>();
        var customer = await customerRepo.GetByIdAsync(id);

        if (customer == null)
            throw new KeyNotFoundException($"Cliente con ID {id} no encontrado");

        if (dto.FullName != null) customer.FullName = dto.FullName;
        if (dto.Dni.HasValue) customer.Dni = dto.Dni.Value;
        if (dto.Phone != null) customer.Phone = dto.Phone;
        if (dto.Email != null) customer.Email = dto.Email;
        
        customer.UpdatedAt = DateTime.UtcNow;

        customerRepo.Update(customer);
        await _unitOfWork.SaveChangesAsync();

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
