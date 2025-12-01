using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Repositories.Inventory;
using Domain.Interfaces.Repositories.Sales;

namespace Application.Features.Sales.Handlers;

/// <summary>
/// Procesa una venta: calcula totales, descuenta stock y registra la venta.
/// </summary>
public class ProcessSaleUseCase
{
    private readonly ISalesRepository _salesRepository;
    private readonly IInventoryRepository _inventoryRepository;

    public ProcessSaleUseCase(
        ISalesRepository salesRepository,
        IInventoryRepository inventoryRepository)
    {
        _salesRepository = salesRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task<SaleDto> ExecuteAsync(CreateSaleDto dto)
    {
        decimal subtotal = 0;

        foreach (var item in dto.Details)
            subtotal += item.Quantity * item.UnitPrice;

        decimal total = subtotal - dto.Discount + dto.Tax;

        // Descontar stock
        foreach (var item in dto.Details)
            await _inventoryRepository.DecreaseStockAsync(item.ProductId, item.Quantity);

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            CustomerId = dto.CustomerId,
            Subtotal = subtotal,
            Total = total,
            Discount = dto.Discount,
            Tax = dto.Tax,
            Status = "Completed"
        };

        var created = await _salesRepository.CreateAsync(sale);

        return new SaleDto
        {
            Id = created.Id,
            Date = created.Date,
            CustomerId = created.CustomerId,
            Subtotal = created.Subtotal,
            Total = created.Total,
            Discount = created.Discount,
            Tax = created.Tax,
            Status = created.Status
        };
    }
}
