using Domain.Entities;

namespace Domain.Interfaces.Repositories.Rentals;

// Interface para el repositorio de alquileres
public interface IRentalRepository
{
    // Obtiene todos los alquileres
    Task<IEnumerable<Rental>> GetAllAsync();
    
    // Obtiene todos los alquileres con sus relaciones
    Task<IEnumerable<Rental>> GetAllWithRelationsAsync();
    
    // Obtiene todos los alquileres activos
    Task<IEnumerable<Rental>> GetActiveAsync();
    
    // Obtiene un alquiler por su ID
    Task<Rental?> GetByIdAsync(Guid id);
    
    // Obtiene un alquiler por su ID con sus relaciones
    Task<Rental?> GetByIdWithRelationsAsync(Guid id);
    
    // Obtiene alquileres por el ID del cliente
    Task<IEnumerable<Rental>> GetByCustomerIdAsync(Guid customerId);
    
    // Obtiene alquileres por el ID del lugar
    Task<IEnumerable<Rental>> GetByPlaceIdAsync(Guid placeId);
    
    // Obtiene alquileres dentro de un rango de fechas
    Task<IEnumerable<Rental>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    
    // Verifica si hay solapamientos en las fechas de alquiler para un lugar específico
    Task<Rental?> CheckOverlapAsync(Guid placeId, DateTime startDate, DateTime endDate, Guid? excludeRentalId = null);
    
    // Crea un nuevo alquiler
    Task<Rental> CreateAsync(Rental rental);
    
    // Actualiza un alquiler existente
    Task<Rental> UpdateAsync(Rental rental);
    
    // Elimina un alquiler por su ID
    Task<bool> DeleteAsync(Guid id);
    
    // Cambia el estado activo/inactivo de un alquiler por su ID
    Task<Rental> ToggleStatusAsync(Guid id);
}
