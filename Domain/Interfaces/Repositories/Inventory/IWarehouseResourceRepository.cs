using Domain.Entities;

namespace Domain.Interfaces.Repositories.Inventory;

/// <summary>
/// Repositorio para WarehouseResource (Stock de recursos en almacenes)
/// </summary>
public interface IWarehouseResourceRepository : IRepository<WarehouseResource>
{
    /// <summary>
    /// Obtener stock de un recurso en un almacén específico
    /// </summary>
    Task<WarehouseResource?> GetStockAsync(Guid warehouseId, Guid resourceId);

    /// <summary>
    /// Obtener todos los recursos en un almacén
    /// </summary>
    Task<IEnumerable<WarehouseResource>> GetByWarehouseIdAsync(Guid warehouseId);

    /// <summary>
    /// Obtener todos los almacenes que tienen un recurso
    /// </summary>
    Task<IEnumerable<WarehouseResource>> GetByResourceIdAsync(Guid resourceId);

    /// <summary>
    /// Obtener recursos con stock activo
    /// </summary>
    Task<IEnumerable<WarehouseResource>> GetActiveStockAsync();

    /// <summary>
    /// Actualizar cantidad de stock de recurso
    /// </summary>
    Task UpdateStockQuantityAsync(Guid warehouseId, Guid resourceId, int quantity);

    /// <summary>
    /// Obtener stock total de un recurso en todos los almacenes
    /// </summary>
    Task<int> GetTotalStockByResourceAsync(Guid resourceId);

    /// <summary>
    /// Verificar si hay stock suficiente de un recurso
    /// </summary>
    Task<bool> HasSufficientStockAsync(Guid warehouseId, Guid resourceId, int requiredQuantity);
}
