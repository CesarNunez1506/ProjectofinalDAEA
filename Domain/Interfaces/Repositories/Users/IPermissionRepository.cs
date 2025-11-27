using Domain.Entities;

namespace Domain.Interfaces.Repositories.Users;

public interface IPermissionRepository
{
    Task<Permission?> GetByIdAsync(Guid id);
    Task<IEnumerable<Permission>> GetAllAsync();
    Task<IEnumerable<Permission>> GetByModuleIdAsync(Guid moduleId);
    Task<Permission> CreateAsync(Permission permission);
    Task<Permission> UpdateAsync(Permission permission);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsForModuleAsync(Guid moduleId);
    Task<int> CountAsync();
}
