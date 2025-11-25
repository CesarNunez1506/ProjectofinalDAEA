using Infrastructure.Data.Entities;

namespace Domain.Interfaces.Repositories.Inventory;

/// <summary>
/// Repositorio específico para Product
/// Hereda operaciones básicas de IRepository y agrega métodos específicos del dominio
/// </summary>
public interface IProductRepository : IRepository<Product>
{
    // Métodos específicos de Product que no están en el repositorio genérico

    /// <summary>
    /// Obtener productos por categoría
    /// </summary>
    Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId);

    /// <summary>
    /// Obtener productos con stock bajo (basado en WarehouseProducts)
    /// </summary>
    Task<IEnumerable<Product>> GetLowStockProductsAsync(int minQuantity = 10);

    /// <summary>
    /// Obtener productos activos (Status = true)
    /// </summary>
    Task<IEnumerable<Product>> GetActiveProductsAsync();

    /// <summary>
    /// Obtener productos producibles (Producible = true)
    /// </summary>
    Task<IEnumerable<Product>> GetProducibleProductsAsync();

    /// <summary>
    /// Buscar productos por nombre (búsqueda parcial)
    /// </summary>
    Task<IEnumerable<Product>> SearchByNameAsync(string name);

    /// <summary>
    /// Obtener producto con su categoría y stock actual
    /// </summary>
    Task<Product?> GetProductWithDetailsAsync(Guid productId);
}
