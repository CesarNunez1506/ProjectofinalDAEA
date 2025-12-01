using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories;

/// <summary>
/// Interfaz genérica para operaciones CRUD con soporte para expresiones LINQ
/// Proporciona métodos base que todos los repositorios pueden usar
/// </summary>
/// <typeparam name="T">Entidad del dominio</typeparam>
public interface IGenericRepository<T> where T : class
{
    // ============ QUERIES (READ) ============
    
    /// <summary>
    /// Obtiene todas las entidades sin filtros
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();
    
    /// <summary>
    /// Obtiene todas las entidades con propiedades relacionadas (eager loading)
    /// </summary>
    /// <param name="includeProperties">Propiedades de navegación a incluir, ej: "User.Role", "Category"</param>
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
    
    /// <summary>
    /// Obtiene una entidad por su ID (Guid)
    /// </summary>
    Task<T?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Obtiene una entidad por ID con propiedades relacionadas
    /// </summary>
    Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includeProperties);
    
    /// <summary>
    /// Busca entidades que cumplan una condición (expresión lambda)
    /// Ejemplo: Find(x => x.Name.Contains("Admin") && x.Status == true)
    /// </summary>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    
    /// <summary>
    /// Busca entidades con condición y propiedades relacionadas
    /// </summary>
    Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includeProperties);
    
    /// <summary>
    /// Busca la primera entidad que cumple la condición o null
    /// </summary>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    
    /// <summary>
    /// Busca la primera entidad con condición y propiedades relacionadas
    /// </summary>
    Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includeProperties);
    
    /// <summary>
    /// Verifica si existe alguna entidad que cumpla la condición
    /// Ejemplo: AnyAsync(x => x.Email == "admin@example.com")
    /// </summary>
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    
    /// <summary>
    /// Cuenta las entidades que cumplen la condición
    /// </summary>
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    
    
    // ============ COMMANDS (WRITE) ============
    
    /// <summary>
    /// Agrega una nueva entidad
    /// </summary>
    Task<T> AddAsync(T entity);
    
    /// <summary>
    /// Agrega múltiples entidades en una sola operación
    /// </summary>
    Task AddRangeAsync(IEnumerable<T> entities);
    
    /// <summary>
    /// Actualiza una entidad existente
    /// </summary>
    Task UpdateAsync(T entity);
    
    /// <summary>
    /// Actualiza múltiples entidades
    /// </summary>
    Task UpdateRangeAsync(IEnumerable<T> entities);
    
    /// <summary>
    /// Elimina una entidad
    /// </summary>
    Task DeleteAsync(T entity);
    
    /// <summary>
    /// Elimina una entidad por su ID
    /// </summary>
    Task DeleteByIdAsync(Guid id);
    
    /// <summary>
    /// Elimina múltiples entidades
    /// </summary>
    Task DeleteRangeAsync(IEnumerable<T> entities);
}
