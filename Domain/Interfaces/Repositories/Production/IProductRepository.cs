using Domain.Entities;

namespace Domain.Interfaces.Repositories.Production;

/// <summary>
/// Interfaz para el repositorio de productos
/// Define operaciones CRUD y consultas específicas para productos
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Obtiene todos los productos con su categoría relacionada
    /// </summary>
    Task<IEnumerable<Product>> GetAllWithCategoryAsync();
    
    /// <summary>
    /// Obtiene solo los productos activos
    /// </summary>
    Task<IEnumerable<Product>> GetActiveAsync();
    
    /// <summary>
    /// Obtiene un producto por su ID con su categoría relacionada
    /// </summary>
    Task<Product?> GetByIdWithCategoryAsync(Guid id);
    
    /// <summary>
    /// Obtiene productos por categoría
    /// </summary>
    Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId);
    
    /// <summary>
    /// Obtiene un producto con sus recetas asociadas
    /// </summary>
    Task<Product?> GetWithRecipesAsync(Guid id);
    
    /// <summary>
    /// Crea un nuevo producto
    /// </summary>
    Task<Product> CreateAsync(Product product);
    
    /// <summary>
    /// Actualiza un producto existente
    /// </summary>
    Task<Product> UpdateAsync(Product product);
    
    /// <summary>
    /// Eliminación lógica (soft delete) de un producto
    /// </summary>
    Task<bool> SoftDeleteAsync(Guid id);
    
    /// <summary>
    /// Verifica si existe un producto por ID
    /// </summary>
    Task<bool> ExistsAsync(Guid id);
}
