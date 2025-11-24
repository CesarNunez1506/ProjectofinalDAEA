using Domain.Entities;
using ProductionEntity = Domain.Entities.Production;

namespace Domain.Interfaces.Repositories.Production;

/// <summary>
/// Interfaz para el repositorio de producción
/// Define operaciones CRUD y consultas específicas para registros de producción
/// </summary>
public interface IProductionRepository
{
    /// <summary>
    /// Obtiene todos los registros de producción
    /// </summary>
    Task<IEnumerable<ProductionEntity>> GetAllAsync();
    
    /// <summary>
    /// Obtiene todos los registros de producción con producto y planta relacionados
    /// </summary>
    Task<IEnumerable<ProductionEntity>> GetAllWithRelationsAsync();
    
    /// <summary>
    /// Obtiene solo las producciones activas
    /// </summary>
    Task<IEnumerable<ProductionEntity>> GetActiveAsync();
    
    /// <summary>
    /// Obtiene un registro de producción por su ID
    /// </summary>
    Task<ProductionEntity?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Obtiene un registro de producción por su ID con relaciones
    /// </summary>
    Task<ProductionEntity?> GetByIdWithRelationsAsync(Guid id);
    
    /// <summary>
    /// Obtiene producciones por producto
    /// </summary>
    Task<IEnumerable<ProductionEntity>> GetByProductIdAsync(Guid productId);
    
    /// <summary>
    /// Obtiene producciones por planta
    /// </summary>
    Task<IEnumerable<ProductionEntity>> GetByPlantIdAsync(Guid plantId);
    
    /// <summary>
    /// Obtiene producciones por rango de fechas
    /// </summary>
    Task<IEnumerable<ProductionEntity>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    
    /// <summary>
    /// Crea un nuevo registro de producción
    /// </summary>
    Task<ProductionEntity> CreateAsync(ProductionEntity production);
    
    /// <summary>
    /// Actualiza un registro de producción existente
    /// </summary>
    Task<ProductionEntity> UpdateAsync(ProductionEntity production);
    
    /// <summary>
    /// Cambia el estado isActive de una producción
    /// </summary>
    Task<bool> ToggleActiveStatusAsync(Guid id);
    
    /// <summary>
    /// Verifica si existe una producción por ID
    /// </summary>
    Task<bool> ExistsAsync(Guid id);
}
