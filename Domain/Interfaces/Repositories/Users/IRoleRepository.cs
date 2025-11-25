using Domain.Entities;

namespace Domain.Interfaces.Repositories.Users;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(Guid id);
    Task<Role?> GetByNameAsync(string name);
    Task<IEnumerable<Role>> GetAllAsync();
    Task<Role> CreateAsync(Role role);
    Task<Role> UpdateAsync(Role role);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByNameAsync(string name);
    Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(Guid roleId);
    Task AddPermissionsAsync(Guid roleId, IEnumerable<Guid> permissionIds);
    Task RemovePermissionsAsync(Guid roleId, IEnumerable<Guid> permissionIds);
}
