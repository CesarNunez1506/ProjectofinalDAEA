using Application.DTOs.Museum;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.PaymentMethods.Queries;

// Caso de uso para obtener todos los métodos de pago
public class GetAllPaymentMethodsUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPaymentMethodsUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PaymentMethodDto>> ExecuteAsync()
    {
        var repository = _unitOfWork.GetRepository<PaymentMethod>();

        // Obtener todos los métodos de pago ordenados por nombre
        var paymentMethods = await repository.GetAsync(
            orderBy: q => q.OrderBy(pm => pm.Name)
        );

        // Mapear las entidades a DTOs
        return paymentMethods.Select(pm => new PaymentMethodDto
        {
            Id = pm.Id,
            Name = pm.Name,
            CreatedAt = pm.CreatedAt,
            UpdatedAt = pm.UpdatedAt
        });
    }
}
