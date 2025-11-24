using Domain.Entities;

namespace Domain.Interfaces.Repositories.Production;

/// <summary>
/// Interfaz para el repositorio de producción
/// Define operaciones CRUD y consultas específicas para registros de producción
/// </summary>
public interface IProductionRepository
{
    /// <summary>
    /// Obtiene todos los registros de producción con producto y planta relacionados
    /// </summary>
    Task<IEnumerable<Production>> GetAllWithRelationsAsync();
    
    /// <summary>
    /// Obtiene solo las producciones activas
    /// </summary>
    Task<IEnumerable<Production>> GetActiveAsync();
    
    /// <summary>
    /// Obtiene un registro de producción por su ID con relaciones
    /// </summary>
    Task<Production?> GetByIdWithRelationsAsync(Guid id);
    
    /// <summary>
    /// Obtiene producciones por producto
    /// </summary>
    Task<IEnumerable<Production>> GetByProductIdAsync(Guid productId);
    
    /// <summary>
    /// Obtiene producciones por planta
    /// </summary>
    Task<IEnumerable<Production>> GetByPlantIdAsync(Guid plantId);
    
    /// <summary>
    /// Obtiene producciones por rango de fechas
    /// </summary>
    Task<IEnumerable<Production>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    
    /// <summary>
    /// Crea un nuevo registro de producción
    /// </summary>
    Task<Production> CreateAsync(Production production);
    
    /// <summary>
    /// Actualiza un registro de producción existente
    /// </summary>
    Task<Production> UpdateAsync(Production production);
    
    /// <summary>
    /// Cambia el estado isActive de una producción
    /// </summary>
    Task<bool> ToggleActiveStatusAsync(Guid id);
    
    /// <summary>
    /// Verifica si existe una producción por ID
    /// </summary>
    Task<bool> ExistsAsync(Guid id);
}
