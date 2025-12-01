using Domain.Interfaces.Repositories.Sales;

namespace Application.Features.Sales.Handlers;

/// <summary>
/// Procesa un reembolso de una venta.
/// </summary>
public class ProcessRefundUseCase
{
    private readonly ISalesRepository _salesRepository;

    public ProcessRefundUseCase(ISalesRepository salesRepository)
    {
        _salesRepository = salesRepository;
    }

    public async Task ExecuteAsync(Guid saleId, decimal amount)
    {
        var sale = await _salesRepository.GetByIdAsync(saleId);
        if (sale == null)
            throw new InvalidOperationException("La venta no existe.");

        sale.Status = "Refunded";
        sale.UpdatedAt = DateTime.UtcNow;

        await _salesRepository.UpdateAsync(sale);
    }
}
