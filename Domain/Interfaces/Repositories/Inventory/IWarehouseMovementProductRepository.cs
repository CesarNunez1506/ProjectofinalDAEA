using Infrastructure.Data.Entities;

namespace Domain.Interfaces.Repositories.Inventory;

/// <summary>
/// Repositorio para WarehouseMovementProduct (Movimientos de productos entre almacenes)
/// </summary>
public interface IWarehouseMovementProductRepository : IRepository<WarehouseMovementProduct>
{
    /// <summary>
    /// Obtener movimientos de un almacén específico
    /// </summary>
    Task<IEnumerable<WarehouseMovementProduct>> GetByWarehouseIdAsync(Guid warehouseId);

    /// <summary>
    /// Obtener movimientos de un producto específico
    /// </summary>
    Task<IEnumerable<WarehouseMovementProduct>> GetByProductIdAsync(Guid productId);

    /// <summary>
    /// Obtener movimientos en un rango de fechas
    /// </summary>
    Task<IEnumerable<WarehouseMovementProduct>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Obtener movimientos por tipo (entrada, salida, transferencia, ajuste)
    /// </summary>
    Task<IEnumerable<WarehouseMovementProduct>> GetByMovementTypeAsync(string movementType);

    /// <summary>
    /// Obtener historial de movimientos de un producto en un almacén
    /// </summary>
    Task<IEnumerable<WarehouseMovementProduct>> GetProductHistoryInWarehouseAsync(Guid warehouseId, Guid productId);

    /// <summary>
    /// Obtener movimientos recientes
    /// </summary>
    Task<IEnumerable<WarehouseMovementProduct>> GetRecentMovementsAsync(int count = 20);
}
