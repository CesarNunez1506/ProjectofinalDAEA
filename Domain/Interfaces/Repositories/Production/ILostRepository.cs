using Domain.Entities;

namespace Domain.Interfaces.Repositories.Production;

/// <summary>
/// Interfaz para el repositorio de pérdidas (mermas)
/// Define operaciones CRUD y consultas específicas para registros de pérdidas en producción
/// </summary>
public interface ILostRepository
{
    /// <summary>
    /// Obtiene todos los registros de pérdidas
    /// </summary>
    Task<IEnumerable<Lost>> GetAllAsync();
    
    /// <summary>
    /// Obtiene todos los registros de pérdidas con relaciones
    /// </summary>
    Task<IEnumerable<Lost>> GetAllWithProductionAsync();
    
    /// <summary>
    /// Obtiene un registro de pérdida por su ID
    /// </summary>
    Task<Lost?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Obtiene pérdidas por producción
    /// </summary>
    Task<IEnumerable<Lost>> GetByProductionIdAsync(Guid productionId);
    
    /// <summary>
    /// Obtiene pérdidas por tipo
    /// </summary>
    Task<IEnumerable<Lost>> GetByLostTypeAsync(string lostType);
    
    /// <summary>
    /// Crea un nuevo registro de pérdida
    /// </summary>
    Task<Lost> CreateAsync(Lost lost);
    
    /// <summary>
    /// Actualiza un registro de pérdida existente
    /// </summary>
    Task<Lost> UpdateAsync(Lost lost);
    
    /// <summary>
    /// Eliminación física de un registro de pérdida
    /// </summary>
    Task<bool> DeleteAsync(Guid id);
    
    /// <summary>
    /// Verifica si existe una pérdida por ID
    /// </summary>
    Task<bool> ExistsAsync(Guid id);
}
