using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories;

/// <summary>
/// Repositorio genérico base con operaciones CRUD comunes
/// Usa Expression para consultas flexibles (Recomendación 3 del profesor)
/// </summary>
public interface IRepository<T> where T : class
{
    // Operaciones de Lectura (Queries)
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Buscar entidades usando expresiones lambda
    /// Ejemplo: FindAsync(p => p.CategoryId == 5)
    /// </summary>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Buscar una sola entidad con expresión
    /// Ejemplo: FindOneAsync(p => p.Sku == "ABC123")
    /// </summary>
    Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate);

    // Operaciones de Escritura (Commands)
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task DeleteAsync(T entity);

    // Operaciones útiles
    Task<bool> ExistsAsync(int id);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync();
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);
}
