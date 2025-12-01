using Domain.Interfaces.Repositories.Sales;

namespace Application.UseCases.Sales;

/// <summary>
/// Caso de uso para procesar un reembolso de una venta.
/// </summary>
public class ProcessRefundUseCase
{
    private readonly ISaleRepository _saleRepository;

    public ProcessRefundUseCase(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task ExecuteAsync(Guid saleId, decimal refundAmount)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);

        if (sale == null)
            throw new InvalidOperationException("La venta no existe.");

        if (refundAmount <= 0 || refundAmount > sale.TotalAmount)
            throw new InvalidOperationException("El monto de reembolso no es válido.");

        sale.Status = "REFUNDED";
        sale.TotalAmount -= refundAmount;
        sale.UpdatedAt = DateTime.UtcNow;

        await _saleRepository.UpdateAsync(sale);
    }
}
