using Application.DTOs.Rentals;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Customers;

public class GetAllCustomersUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllCustomersUseCase> _logger;

    public GetAllCustomersUseCase(IUnitOfWork unitOfWork, ILogger<GetAllCustomersUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<CustomerDto>> ExecuteAsync()
    {
        var customerRepo = _unitOfWork.GetRepository<Customer>();
        var customers = await customerRepo.GetAllAsync();

        return customers.Select(c => new CustomerDto
        {
            Id = c.Id,
            FullName = c.FullName,
            Dni = c.Dni,
            Phone = c.Phone,
            Email = c.Email,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        });
    }
}
