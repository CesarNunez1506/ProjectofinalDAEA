using Domain.Entities;

namespace Domain.Interfaces.Repositories.Production;

/// <summary>
/// Interfaz para el repositorio de plantas de producción
/// Define operaciones CRUD y consultas específicas para plantas de producción
/// </summary>
public interface IPlantProductionRepository
{
    /// <summary>
    /// Obtiene todas las plantas de producción
    /// </summary>
    Task<IEnumerable<PlantProduction>> GetAllAsync();
    
    /// <summary>
    /// Obtiene solo las plantas activas
    /// </summary>
    Task<IEnumerable<PlantProduction>> GetActiveAsync();
    
    /// <summary>
    /// Obtiene una planta por su ID
    /// </summary>
    Task<PlantProduction?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Obtiene una planta con su almacén asociado
    /// </summary>
    Task<PlantProduction?> GetByIdWithWarehouseAsync(Guid id);
    
    /// <summary>
    /// Crea una nueva planta de producción
    /// </summary>
    Task<PlantProduction> CreateAsync(PlantProduction plant);
    
    /// <summary>
    /// Actualiza una planta existente
    /// </summary>
    Task<PlantProduction> UpdateAsync(PlantProduction plant);
    
    /// <summary>
    /// Eliminación lógica (soft delete) de una planta
    /// </summary>
    Task<bool> SoftDeleteAsync(Guid id);
    
    /// <summary>
    /// Verifica si existe una planta por ID
    /// </summary>
    Task<bool> ExistsAsync(Guid id);
}
