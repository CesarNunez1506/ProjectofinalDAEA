using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.WarehouseStores.Commands;

public class CreateWarehouseStoreCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateWarehouseStoreCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<WarehouseStoreDto> ExecuteAsync(CreateWarehouseStoreDto dto)
    {
        var repo = _unitOfWork.GetRepository<WarehouseStore>();

        // Verificar si ya existe un registro para esta tienda y producto
        var existing = await repo.FirstOrDefaultAsync(ws => 
            ws.StoreId == dto.StoreId && ws.ProductId == dto.ProductId);

        if (existing != null)
            throw new InvalidOperationException("Ya existe un registro de inventario para este producto en esta tienda");

        // Validar que la tienda existe
        var storeRepo = _unitOfWork.GetRepository<Store>();
        var storeExists = await storeRepo.AnyAsync(s => s.Id == dto.StoreId);
        if (!storeExists)
            throw new KeyNotFoundException($"Tienda con ID {dto.StoreId} no encontrada");

        // Validar que el producto existe
        var productRepo = _unitOfWork.GetRepository<Product>();
        var productExists = await productRepo.AnyAsync(p => p.Id == dto.ProductId);
        if (!productExists)
            throw new KeyNotFoundException($"Producto con ID {dto.ProductId} no encontrado");

        var warehouseStore = new WarehouseStore
        {
            Id = Guid.NewGuid(),
            StoreId = dto.StoreId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            CreatedAt = DateTime.UtcNow
        };

        await repo.AddAsync(warehouseStore);
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
