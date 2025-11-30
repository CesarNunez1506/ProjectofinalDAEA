using Application.DTOs.Rentals;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Customers;

public class GetCustomerByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCustomerByIdUseCase> _logger;

    public GetCustomerByIdUseCase(IUnitOfWork unitOfWork, ILogger<GetCustomerByIdUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<CustomerDto> ExecuteAsync(Guid id)
    {
        var customerRepo = _unitOfWork.GetRepository<Customer>();
        var customer = await customerRepo.GetByIdAsync(id);

        if (customer == null)
            throw new KeyNotFoundException($"Cliente con ID {id} no encontrado");

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
