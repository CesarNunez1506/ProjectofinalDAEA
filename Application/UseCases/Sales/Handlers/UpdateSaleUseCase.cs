using Application.DTOs.Sales;
using Domain.Interfaces.Repositories.Sales;

namespace Application.Features.Sales.Handlers;

/// <summary>
/// Actualiza los datos de una venta.
/// </summary>
public class UpdateSaleUseCase
{
    private readonly ISalesRepository _salesRepository;

    public UpdateSaleUseCase(ISalesRepository salesRepository)
    {
        _salesRepository = salesRepository;
    }

    public async Task<SaleDto> ExecuteAsync(Guid id, UpdateSaleDto dto)
    {
        var existing = await _salesRepository.GetByIdAsync(id);
        if (existing == null)
            throw new InvalidOperationException("La venta no existe.");

        existing.Discount = dto.Discount;
        existing.Tax = dto.Tax;
        existing.Status = dto.Status;
        existing.UpdatedAt = DateTime.UtcNow;

        var updated = await _salesRepository.UpdateAsync(existing);

        return new SaleDto
        {
            Id = updated.Id,
            Date = updated.Date,
            CustomerId = updated.CustomerId,
            Subtotal = updated.Subtotal,
            Total = updated.Total,
            Discount = updated.Discount,
            Tax = updated.Tax,
            Status = updated.Status
        };
    }
}
