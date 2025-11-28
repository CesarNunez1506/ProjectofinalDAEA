using Domain.Entities;

namespace Domain.Interfaces.Repositories.Finance;
// Interface para el repositorio de módulos financieros
public interface IModuleRepository
{   
    // Obtiene todos los módulos financieros
    Task<IEnumerable<Module>> GetAllAsync();
    
    // Obtiene un módulo financiero por su ID
    Task<Module?> GetByIdAsync(Guid id);
    
    // Obtiene un módulo financiero por su nombre
    Task<Module?> GetByNameAsync(string name);
}
