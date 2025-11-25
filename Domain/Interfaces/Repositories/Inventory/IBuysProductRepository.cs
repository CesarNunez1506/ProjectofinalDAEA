using Domain.Entities;

namespace Domain.Interfaces.Repositories.Inventory;

/// <summary>
/// Repositorio para BuysProduct (Órdenes de compra de productos)
/// </summary>
public interface IBuysProductRepository : IRepository<BuysProduct>
{
    /// <summary>
    /// Obtener compras por proveedor
    /// </summary>
    Task<IEnumerable<BuysProduct>> GetBySupplierIdAsync(Guid supplierId);

    /// <summary>
    /// Obtener compras por almacén
    /// </summary>
    Task<IEnumerable<BuysProduct>> GetByWarehouseIdAsync(Guid warehouseId);

    /// <summary>
    /// Obtener compras en un rango de fechas
    /// </summary>
    Task<IEnumerable<BuysProduct>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Obtener compra con todos sus detalles (productos comprados)
    /// </summary>
    Task<BuysProduct?> GetPurchaseWithDetailsAsync(Guid purchaseId);

    /// <summary>
    /// Obtener compras activas
    /// </summary>
    Task<IEnumerable<BuysProduct>> GetActivePurchasesAsync();

    /// <summary>
    /// Obtener total de compras por proveedor en un período
    /// </summary>
    Task<decimal> GetTotalPurchasesBySupplierAsync(Guid supplierId, DateTime startDate, DateTime endDate);

    /// <summary>
    /// Obtener últimas compras
    /// </summary>
    Task<IEnumerable<BuysProduct>> GetRecentPurchasesAsync(int count = 10);
}
