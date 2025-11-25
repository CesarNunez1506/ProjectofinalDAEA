using Domain.Entities;

namespace Domain.Interfaces.Repositories.Inventory;

/// <summary>
/// Repositorio para WarehouseMovementResource (Movimientos de recursos entre almacenes)
/// </summary>
public interface IWarehouseMovementResourceRepository : IRepository<WarehouseMovementResource>
{
    /// <summary>
    /// Obtener movimientos de un almacén específico
    /// </summary>
    Task<IEnumerable<WarehouseMovementResource>> GetByWarehouseIdAsync(Guid warehouseId);

    /// <summary>
    /// Obtener movimientos de un recurso específico
    /// </summary>
    Task<IEnumerable<WarehouseMovementResource>> GetByResourceIdAsync(Guid resourceId);

    /// <summary>
    /// Obtener movimientos en un rango de fechas
    /// </summary>
    Task<IEnumerable<WarehouseMovementResource>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Obtener movimientos por tipo
    /// </summary>
    Task<IEnumerable<WarehouseMovementResource>> GetByMovementTypeAsync(string movementType);

    /// <summary>
    /// Obtener historial de movimientos de un recurso en un almacén
    /// </summary>
    Task<IEnumerable<WarehouseMovementResource>> GetResourceHistoryInWarehouseAsync(Guid warehouseId, Guid resourceId);

    /// <summary>
    /// Obtener movimientos recientes
    /// </summary>
    Task<IEnumerable<WarehouseMovementResource>> GetRecentMovementsAsync(int count = 20);
}
