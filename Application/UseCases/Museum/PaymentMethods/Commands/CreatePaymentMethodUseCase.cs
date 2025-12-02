using Application.DTOs.Museum;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.PaymentMethods.Commands;

// Caso de uso para crear un nuevo método de pago
public class CreatePaymentMethodUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePaymentMethodUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentMethodDto> ExecuteAsync(CreatePaymentMethodDto dto)
    {
        var repository = _unitOfWork.GetRepository<PaymentMethod>();

        // Crear el nuevo método de pago
        var paymentMethod = new PaymentMethod
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Agregar el nuevo método de pago al repositorio
        await repository.AddAsync(paymentMethod);
        await _unitOfWork.SaveChangesAsync();

        // Retornar el DTO del método de pago creado
        return new PaymentMethodDto
        {
            Id = paymentMethod.Id,
            Name = paymentMethod.Name,
            CreatedAt = paymentMethod.CreatedAt,
            UpdatedAt = paymentMethod.UpdatedAt
        };
    }
}
