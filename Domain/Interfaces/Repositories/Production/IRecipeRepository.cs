using Domain.Entities;

namespace Domain.Interfaces.Repositories.Production;

/// <summary>
/// Interfaz para el repositorio de recetas
/// Define operaciones CRUD y consultas específicas para recetas de producción
/// </summary>
public interface IRecipeRepository
{
    /// <summary>
    /// Obtiene todas las recetas con sus productos y recursos relacionados
    /// </summary>
    Task<IEnumerable<Recipe>> GetAllWithRelationsAsync();
    
    /// <summary>
    /// Obtiene una receta por su ID
    /// </summary>
    Task<Recipe?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Obtiene una receta por su ID con relaciones (Product, Resource)
    /// </summary>
    Task<Recipe?> GetByIdWithRelationsAsync(Guid id);
    
    /// <summary>
    /// Obtiene todas las recetas de un producto específico con los recursos relacionados
    /// </summary>
    Task<IEnumerable<Recipe>> GetByProductIdAsync(Guid productId);
    
    /// <summary>
    /// Verifica si ya existe una receta para la combinación producto-recurso
    /// </summary>
    Task<bool> ExistsByProductAndResourceAsync(Guid productId, Guid resourceId);
    
    /// <summary>
    /// Crea una nueva receta
    /// </summary>
    Task<Recipe> CreateAsync(Recipe recipe);
    
    /// <summary>
    /// Actualiza una receta existente (solo quantity y unit)
    /// </summary>
    Task<Recipe> UpdateAsync(Recipe recipe);
    
    /// <summary>
    /// Eliminación física de una receta
    /// </summary>
    Task<bool> DeleteAsync(Guid id);
    
    /// <summary>
    /// Verifica si existe una receta por ID
    /// </summary>
    Task<bool> ExistsAsync(Guid id);
}
