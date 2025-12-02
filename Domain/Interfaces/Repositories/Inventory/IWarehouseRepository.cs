using Domain.Entities;

namespace Domain.Interfaces.Repositories.Inventory;

/// <summary>
/// Repositorio específico para Warehouse (Almacenes)
/// </summary>
public interface IWarehouseRepository : IRepository<Warehouse>
{
    /// <summary>
    /// Obtener almacenes activos
    /// </summary>
    Task<IEnumerable<Warehouse>> GetActiveWarehousesAsync();

    /// <summary>
    /// Obtener almacén con sus productos (stock actual)
    /// </summary>
    Task<Warehouse?> GetWarehouseWithProductsAsync(Guid warehouseId);

    /// <summary>
    /// Obtener almacén con sus recursos
    /// </summary>
    Task<Warehouse?> GetWarehouseWithResourcesAsync(Guid warehouseId);

    /// <summary>
    /// Obtener stock de un producto en un almacén específico
    /// </summary>
    Task<int> GetProductStockAsync(Guid warehouseId, Guid productId);

    /// <summary>
    /// Obtener stock de un recurso en un almacén específico
    /// </summary>
    Task<int> GetResourceStockAsync(Guid warehouseId, Guid resourceId);

    /// <summary>
    /// Buscar almacenes por ubicación
    /// </summary>
    Task<IEnumerable<Warehouse>> SearchByLocationAsync(string location);

    /// <summary>
    /// Verificar si un almacén tiene capacidad disponible
    /// </summary>
    Task<bool> HasCapacityAsync(Guid warehouseId);
}
