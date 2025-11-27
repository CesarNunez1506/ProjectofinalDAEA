using Domain.Entities;

namespace Domain.Interfaces.Repositories.Rentals;

// Interface para el repositorio de clientes
public interface ICustomerRepository
{
    // Obtiene todos los clientes
    Task<IEnumerable<Customer>> GetAllAsync();
    
    // Obtiene un cliente por su ID
    Task<Customer?> GetByIdAsync(Guid id);
    
    // Obtiene un cliente por su DNI
    Task<Customer?> GetByDniAsync(int dni);
    
    // Obtiene un cliente por su correo electrónico
    Task<Customer?> GetByEmailAsync(string email);
    
    // Crea un nuevo cliente
    Task<Customer> CreateAsync(Customer customer);
    
    // Actualiza un cliente existente
    Task<Customer> UpdateAsync(Customer customer);
    
    // Elimina un cliente por su ID
    Task<bool> DeleteAsync(Guid id);
}
