using Domain.Entities;

namespace Domain.Interfaces.Repositories.Finance;

// Interface para el repositorio de ingresos generales
public interface IGeneralIncomeRepository
{
    // Crea un nuevo ingreso general
    Task<GeneralIncome> CreateAsync(GeneralIncome income);
    
    // Obtiene todos los ingresos generales
    Task<IEnumerable<GeneralIncome>> GetAllAsync();
    
    // Obtiene un ingreso general por su ID
    Task<GeneralIncome?> GetByIdAsync(Guid id);
}
