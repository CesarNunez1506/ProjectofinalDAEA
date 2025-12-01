using Application.DTOs.Sales;
using Domain.Interfaces.Repositories.Sales;

namespace Application.UseCases.Sales;

public class UpdateSaleUseCase
{
    private readonly ISaleRepository _saleRepository;

    public UpdateSaleUseCase(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<SaleDto> ExecuteAsync(Guid saleId, UpdateSaleDto dto)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale == null)
            throw new InvalidOperationException("La venta no existe.");

        if (sale.Status == "CANCELLED")
            throw new InvalidOperationException("No se puede actualizar una venta cancelada.");

        sale.CustomerId = dto.CustomerId ?? sale.CustomerId;
        sale.UpdatedAt = DateTime.UtcNow;

        var updated = await _saleRepository.UpdateAsync(sale);

        return new SaleDto
        {
            Id = updated.Id,
            StoreId = updated.StoreId,
            CustomerId = updated.CustomerId,
            TotalAmount = updated.TotalAmount,
            Status = updated.Status,
            CreatedAt = updated.CreatedAt
        };
    }
}
