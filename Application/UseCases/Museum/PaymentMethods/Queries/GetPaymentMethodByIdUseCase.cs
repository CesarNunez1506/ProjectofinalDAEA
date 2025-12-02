using Application.DTOs.Museum;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.PaymentMethods.Queries;

// Caso de uso para obtener un método de pago por ID
public class GetPaymentMethodByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPaymentMethodByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentMethodDto> ExecuteAsync(Guid id)
    {
        var repository = _unitOfWork.GetRepository<PaymentMethod>();

        // Obtener el método de pago por ID
        var paymentMethod = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Método de pago con ID {id} no encontrado");

        // Retornar el DTO del método de pago
        return new PaymentMethodDto
        {
            Id = paymentMethod.Id,
            Name = paymentMethod.Name,
            CreatedAt = paymentMethod.CreatedAt,
            UpdatedAt = paymentMethod.UpdatedAt
        };
    }
}
