using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.WarehouseStores.Commands;

public class UpdateWarehouseStoreCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateWarehouseStoreCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<WarehouseStoreDto> ExecuteAsync(Guid id, UpdateWarehouseStoreDto dto)
    {
        var repo = _unitOfWork.GetRepository<WarehouseStore>();

        var warehouseStore = await repo.GetByIdAsync(id);
        if (warehouseStore == null)
            throw new KeyNotFoundException($"Registro de inventario con ID {id} no encontrado");

        warehouseStore.StoreId = dto.StoreId;
        warehouseStore.ProductId = dto.ProductId;
        warehouseStore.Quantity = dto.Quantity;
        warehouseStore.UpdatedAt = DateTime.UtcNow;

        repo.Update(warehouseStore);
        await _unitOfWork.SaveChangesAsync();

        return new WarehouseStoreDto
        {
            Id = warehouseStore.Id,
            StoreId = warehouseStore.StoreId,
            ProductId = warehouseStore.ProductId,
            Quantity = warehouseStore.Quantity,
            CreatedAt = warehouseStore.CreatedAt,
            UpdatedAt = warehouseStore.UpdatedAt
        };
    }
}
