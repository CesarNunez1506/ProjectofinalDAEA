using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Customers;

public class DeleteCustomerUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteCustomerUseCase> _logger;

    public DeleteCustomerUseCase(IUnitOfWork unitOfWork, ILogger<DeleteCustomerUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var customerRepo = _unitOfWork.GetRepository<Customer>();
        var customer = await customerRepo.GetByIdAsync(id);

        if (customer == null)
            throw new KeyNotFoundException($"Cliente con ID {id} no encontrado");

        customerRepo.Remove(customer);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
