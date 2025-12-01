using Domain.Interfaces.Repositories.Inventory;
using Application.DTOs.Sales;

namespace Application.Features.Sales.Handlers;

/// <summary>
/// Caso de uso para descontar stock de todos los productos de una venta.
/// </summary>
public class DiscountStockUseCase
{
    private readonly IInventoryRepository _inventoryRepository;

    public DiscountStockUseCase(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task ExecuteAsync(CreateSaleDto dto)
    {
        foreach (var item in dto.Details)
            await _inventoryRepository.DecreaseStockAsync(item.ProductId, item.Quantity);
    }
}
