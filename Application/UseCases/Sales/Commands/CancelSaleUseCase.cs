using Domain.Interfaces.Repositories.Sales;

namespace Application.UseCases.Sales;

public class CancelSaleUseCase
{
    private readonly ISaleRepository _saleRepository;

    public CancelSaleUseCase(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task ExecuteAsync(Guid saleId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);

        if (sale == null)
            throw new InvalidOperationException("La venta no existe.");

        if (sale.Status == "CANCELLED")
            throw new InvalidOperationException("La venta ya está cancelada.");

        sale.Status = "CANCELLED";
        sale.UpdatedAt = DateTime.UtcNow;

        await _saleRepository.UpdateAsync(sale);
    }
}
