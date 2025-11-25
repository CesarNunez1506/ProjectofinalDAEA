using Domain.Entities;

namespace Domain.Interfaces.Repositories.Inventory;

/// <summary>
/// Repositorio específico para Resource (Recursos/Materias primas)
/// </summary>
public interface IResourceRepository : IRepository<Resource>
{
    /// <summary>
    /// Obtener recursos activos
    /// </summary>
    Task<IEnumerable<Resource>> GetActiveResourcesAsync();

    /// <summary>
    /// Obtener recursos por proveedor
    /// </summary>
    Task<IEnumerable<Resource>> GetBySupplierIdAsync(Guid supplierId);

    /// <summary>
    /// Buscar recursos por nombre
    /// </summary>
    Task<IEnumerable<Resource>> SearchByNameAsync(string name);

    /// <summary>
    /// Obtener recurso con su proveedor
    /// </summary>
    Task<Resource?> GetResourceWithSupplierAsync(Guid resourceId);

    /// <summary>
    /// Obtener recursos con stock bajo en almacenes
    /// </summary>
    Task<IEnumerable<Resource>> GetLowStockResourcesAsync(int minQuantity = 10);

    /// <summary>
    /// Obtener recursos usados en recetas
    /// </summary>
    Task<IEnumerable<Resource>> GetResourcesUsedInRecipesAsync();
}
