using Domain.Entities;

namespace Domain.Interfaces.Repositories.Rentals;

// Interface para el repositorio de ubicaciones
public interface ILocationRepository
{
    // Obtiene todas las ubicaciones
    Task<IEnumerable<Location>> GetAllAsync();
    
    // Obtiene una ubicación por su ID
    Task<Location?> GetByIdAsync(Guid id);
    
    // Obtiene ubicaciones por su estado
    Task<IEnumerable<Location>> GetByStatusAsync(string status);
    
    // Crea una nueva ubicación
    Task<Location> CreateAsync(Location location);
    
    // Actualiza una ubicación existente
    Task<Location> UpdateAsync(Location location);
    
    // Elimina una ubicación por su ID  
    Task<bool> DeleteAsync(Guid id);
}
