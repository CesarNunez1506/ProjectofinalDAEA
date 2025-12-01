using Domain.Interfaces.Repositories.Sales;

namespace Application.Features.Sales.Handlers;

/// <summary>
/// Cancela una venta existente.
/// </summary>
public class CancelSaleUseCase
{
    private readonly ISalesRepository _salesRepository;

    public CancelSaleUseCase(ISalesRepository salesRepository)
    {
        _salesRepository = salesRepository;
    }

    public async Task ExecuteAsync(Guid id)
    {
        var sale = await _salesRepository.GetByIdAsync(id);
        if (sale == null)
            throw new InvalidOperationException("La venta no existe.");

        sale.Status = "Cancelled";
        sale.UpdatedAt = DateTime.UtcNow;

        await _salesRepository.UpdateAsync(sale);
    }
}
