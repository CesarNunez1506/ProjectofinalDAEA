using Infrastructure.Data.Entities;

namespace Domain.Interfaces.Repositories.Inventory;

/// <summary>
/// Repositorio específico para Supplier (Proveedores)
/// </summary>
public interface ISupplierRepository : IRepository<Supplier>
{
    /// <summary>
    /// Obtener proveedores activos
    /// </summary>
    Task<IEnumerable<Supplier>> GetActiveSuppliersAsync();

    /// <summary>
    /// Buscar proveedor por RUC
    /// </summary>
    Task<Supplier?> GetByRucAsync(long ruc);

    /// <summary>
    /// Buscar proveedores por nombre
    /// </summary>
    Task<IEnumerable<Supplier>> SearchByNameAsync(string name);

    /// <summary>
    /// Buscar proveedor por email
    /// </summary>
    Task<Supplier?> GetByEmailAsync(string email);

    /// <summary>
    /// Obtener proveedor con sus recursos
    /// </summary>
    Task<Supplier?> GetSupplierWithResourcesAsync(Guid supplierId);

    /// <summary>
    /// Obtener proveedor con historial de compras
    /// </summary>
    Task<Supplier?> GetSupplierWithPurchasesAsync(Guid supplierId);

    /// <summary>
    /// Verificar si ya existe un proveedor con el mismo RUC
    /// </summary>
    Task<bool> ExistsByRucAsync(long ruc);
}
