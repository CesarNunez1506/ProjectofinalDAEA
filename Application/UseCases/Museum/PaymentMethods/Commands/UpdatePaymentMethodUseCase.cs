using Application.DTOs.Museum;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.PaymentMethods.Commands;

// Caso de uso para actualizar un método de pago existente
public class UpdatePaymentMethodUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePaymentMethodUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentMethodDto> ExecuteAsync(Guid id, UpdatePaymentMethodDto dto)
    {
        var repository = _unitOfWork.GetRepository<PaymentMethod>();

        // Obtener el método de pago existente
        var paymentMethod = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Método de pago con ID {id} no encontrado");

        // Actualizar los campos proporcionados
        if (dto.Name is not null)
            paymentMethod.Name = dto.Name;

        paymentMethod.UpdatedAt = DateTime.UtcNow;

        // Guardar los cambios
        repository.Update(paymentMethod);
        await _unitOfWork.SaveChangesAsync();

        // Retornar el DTO del método de pago actualizado
        return new PaymentMethodDto
        {
            Id = paymentMethod.Id,
            Name = paymentMethod.Name,
            CreatedAt = paymentMethod.CreatedAt,
            UpdatedAt = paymentMethod.UpdatedAt
        };
    }
}
