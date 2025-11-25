using Domain.Entities;

namespace Domain.Interfaces.Repositories.Inventory;

/// <summary>
/// Repositorio para WarehouseProduct (Stock de productos en almacenes)
/// </summary>
public interface IWarehouseProductRepository : IRepository<WarehouseProduct>
{
    /// <summary>
    /// Obtener stock de un producto en un almacén específico
    /// </summary>
    Task<WarehouseProduct?> GetStockAsync(Guid warehouseId, Guid productId);

    /// <summary>
    /// Obtener todos los productos en un almacén
    /// </summary>
    Task<IEnumerable<WarehouseProduct>> GetByWarehouseIdAsync(Guid warehouseId);

    /// <summary>
    /// Obtener todos los almacenes que tienen un producto
    /// </summary>
    Task<IEnumerable<WarehouseProduct>> GetByProductIdAsync(Guid productId);

    /// <summary>
    /// Obtener productos con stock activo
    /// </summary>
    Task<IEnumerable<WarehouseProduct>> GetActiveStockAsync();

    /// <summary>
    /// Actualizar cantidad de stock
    /// </summary>
    Task UpdateStockQuantityAsync(Guid warehouseId, Guid productId, int quantity);

    /// <summary>
    /// Obtener stock total de un producto en todos los almacenes
    /// </summary>
    Task<int> GetTotalStockByProductAsync(Guid productId);

    /// <summary>
    /// Verificar si hay stock suficiente
    /// </summary>
    Task<bool> HasSufficientStockAsync(Guid warehouseId, Guid productId, int requiredQuantity);
}
