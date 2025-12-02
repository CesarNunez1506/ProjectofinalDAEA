using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.PaymentMethods.Commands;

// Caso de uso para eliminar un método de pago
public class DeletePaymentMethodUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePaymentMethodUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid id)
    {
        var repository = _unitOfWork.GetRepository<PaymentMethod>();

        // Obtener el método de pago existente
        var paymentMethod = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Método de pago con ID {id} no encontrado");

        // Eliminar el método de pago
        repository.Remove(paymentMethod);
        await _unitOfWork.SaveChangesAsync();
    }
}
