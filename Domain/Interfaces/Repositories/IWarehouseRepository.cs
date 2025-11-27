using Domain.Entities;

namespace Domain.Interfaces.Repositories;

/// <summary>
/// Interfaz para operaciones de almacén requeridas por el módulo de producción
/// </summary>
public interface IWarehouseRepository
{
    // Warehouse Products
    Task<WarehouseProduct?> GetWarehouseProductAsync(Guid warehouseId, Guid productId);
    Task AddWarehouseProductAsync(WarehouseProduct warehouseProduct);
    Task UpdateWarehouseProductAsync(WarehouseProduct warehouseProduct);
    
    // Warehouse Movements Products
    Task AddWarehouseMovementProductAsync(WarehouseMovementProduct movement);
    
    // Warehouse Resources
    Task<List<WarehouseResource>> GetWarehouseResourcesByResourceIdAsync(Guid warehouseId, Guid resourceId);
    Task UpdateWarehouseResourceAsync(WarehouseResource warehouseResource);
    
    // Warehouse Movements Resources
    Task AddWarehouseMovementResourceAsync(WarehouseMovementResource movement);
    
    // Resources
    Task<Resource?> GetResourceByIdAsync(Guid resourceId);
    
    // Save Changes
    Task<int> SaveChangesAsync();
}
