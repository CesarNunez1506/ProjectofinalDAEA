using Domain.Entities;

namespace Domain.Interfaces.Repositories.Inventory;

/// <summary>
/// Repositorio específico para Category
/// </summary>
public interface ICategoryRepository : IRepository<Category>
{
    /// <summary>
    /// Obtener categorías activas
    /// </summary>
    Task<IEnumerable<Category>> GetActiveCategoriesAsync();

    /// <summary>
    /// Obtener categoría con todos sus productos
    /// </summary>
    Task<Category?> GetCategoryWithProductsAsync(Guid categoryId);

    /// <summary>
    /// Buscar categorías por nombre
    /// </summary>
    Task<IEnumerable<Category>> SearchByNameAsync(string name);

    /// <summary>
    /// Verificar si una categoría tiene productos asociados
    /// </summary>
    Task<bool> HasProductsAsync(Guid categoryId);
}
