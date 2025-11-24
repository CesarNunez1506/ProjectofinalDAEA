using Domain.Entities;

namespace Domain.Interfaces.Repositories.Production;

/// <summary>
/// Interfaz para el repositorio de categorías
/// Define operaciones CRUD y consultas específicas para categorías de productos
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Obtiene todas las categorías (incluyendo inactivas)
    /// </summary>
    Task<IEnumerable<Category>> GetAllAsync();
    
    /// <summary>
    /// Obtiene solo las categorías activas
    /// </summary>
    Task<IEnumerable<Category>> GetActiveAsync();
    
    /// <summary>
    /// Obtiene una categoría por su ID
    /// </summary>
    Task<Category?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Obtiene una categoría por su nombre
    /// </summary>
    Task<Category?> GetByNameAsync(string name);
    
    /// <summary>
    /// Verifica si una categoría tiene productos activos asociados
    /// </summary>
    Task<bool> HasActiveProductsAsync(Guid categoryId);
    
    /// <summary>
    /// Crea una nueva categoría
    /// </summary>
    Task<Category> CreateAsync(Category category);
    
    /// <summary>
    /// Actualiza una categoría existente
    /// </summary>
    Task<Category> UpdateAsync(Category category);
    
    /// <summary>
    /// Eliminación lógica (soft delete) de una categoría
    /// </summary>
    Task<bool> SoftDeleteAsync(Guid id);
    
    /// <summary>
    /// Verifica si existe una categoría por ID
    /// </summary>
    Task<bool> ExistsAsync(Guid id);
}
