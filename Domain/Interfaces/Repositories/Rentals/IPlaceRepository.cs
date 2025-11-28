using Domain.Entities;

namespace Domain.Interfaces.Repositories.Rentals;

// Interface para el repositorio de lugares
public interface IPlaceRepository
{
    // Obtiene todos los lugares
    Task<IEnumerable<Place>> GetAllAsync();
    
    // Obtiene todos los lugares con sus relaciones
    Task<IEnumerable<Place>> GetAllWithRelationsAsync();
    
    // Obtiene un lugar por su ID
    Task<Place?> GetByIdAsync(Guid id);
    
    // Obtiene un lugar por su ID con sus relaciones
    Task<Place?> GetByIdWithRelationsAsync(Guid id);
    
    // Obtiene lugares por el ID de la ubicación
    Task<IEnumerable<Place>> GetByLocationIdAsync(Guid locationId);
    
    // Crea un nuevo lugar
    Task<Place> CreateAsync(Place place);
    
    // Actualiza un lugar existente
    Task<Place> UpdateAsync(Place place);
    
    // Elimina un lugar por su ID
    Task<bool> DeleteAsync(Guid id);
}
