using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.Returns.Commands;

public class CreateReturnCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateReturnCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ReturnDto> ExecuteAsync(CreateReturnDto dto)
    {
        var repo = _unitOfWork.GetRepository<Return>();

        // Validar que el producto existe si se proporciona
        if (dto.ProductId.HasValue)
        {
            var productRepo = _unitOfWork.GetRepository<Product>();
            var product = await productRepo.GetByIdAsync(dto.ProductId.Value);
            if (product == null)
                throw new KeyNotFoundException($"Producto con ID {dto.ProductId} no encontrado");
        }

        // Si es una devolución (no pérdida), validar que la venta existe
        if (dto.SalesId.HasValue)
        {
            var saleRepo = _unitOfWork.GetRepository<Sale>();
            var saleExists = await saleRepo.AnyAsync(s => s.Id == dto.SalesId.Value);
            if (!saleExists)
                throw new KeyNotFoundException($"Venta con ID {dto.SalesId} no encontrada");
        }

        // Validar que la tienda existe si se proporciona
        if (dto.StoreId.HasValue)
        {
            var storeRepo = _unitOfWork.GetRepository<Store>();
            var storeExists = await storeRepo.AnyAsync(s => s.Id == dto.StoreId.Value);
            if (!storeExists)
                throw new KeyNotFoundException($"Tienda con ID {dto.StoreId} no encontrada");
        }

        var returnEntity = new Return
        {
            Id = Guid.NewGuid(),
            ProductId = dto.ProductId,
            SalesId = dto.SalesId,
            StoreId = dto.StoreId,
            Reason = dto.Reason,
            Observations = dto.Observations,
            Quantity = dto.Quantity,
            Price = dto.Price,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await repo.AddAsync(returnEntity);

        // Devolver stock al inventario de la tienda si se tienen ProductId y StoreId
        if (dto.ProductId.HasValue && dto.StoreId.HasValue)
        {
            var warehouseStoreRepo = _unitOfWork.GetRepository<WarehouseStore>();
            var inventoryItem = await warehouseStoreRepo.FirstOrDefaultAsync(ws => 
                ws.StoreId == dto.StoreId.Value && ws.ProductId == dto.ProductId.Value);

            if (inventoryItem != null)
            {
                inventoryItem.Quantity += dto.Quantity;
                inventoryItem.UpdatedAt = DateTime.UtcNow;
                warehouseStoreRepo.Update(inventoryItem);
            }
            else
            {
                // Crear nuevo registro de inventario si no existe
                var newInventoryItem = new WarehouseStore
                {
                    Id = Guid.NewGuid(),
                    StoreId = dto.StoreId.Value,
                    ProductId = dto.ProductId.Value,
                    Quantity = dto.Quantity,
                    CreatedAt = DateTime.UtcNow
                };
                await warehouseStoreRepo.AddAsync(newInventoryItem);
            }
        }

        await _unitOfWork.SaveChangesAsync();

        // TODO: Registrar gasto en general_expenses según la lógica de Node.js

        return new ReturnDto
        {
            Id = returnEntity.Id,
            ProductId = returnEntity.ProductId,
            SalesId = returnEntity.SalesId,
            StoreId = returnEntity.StoreId,
            Reason = returnEntity.Reason,
            Observations = returnEntity.Observations,
            Quantity = returnEntity.Quantity,
            Price = returnEntity.Price,
            CreatedAt = returnEntity.CreatedAt,
            UpdatedAt = returnEntity.UpdatedAt
        };
    }
}
