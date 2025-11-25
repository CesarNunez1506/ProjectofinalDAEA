using Domain.Entities;

namespace Domain.Interfaces.Repositories.Users;

public interface IModuleRepository
{
    Task<Module?> GetByIdAsync(Guid id);
    Task<Module?> GetByNameAsync(string name);
    Task<IEnumerable<Module>> GetAllAsync();
    Task<Module> CreateAsync(Module module);
    Task<Module> UpdateAsync(Module module);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsByNameAsync(string name);
}
