using Domain.Entities;

namespace Domain.Interfaces.Repositories.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByDniAsync(string dni);
    Task<IEnumerable<User>> GetAllAsync();
    Task<IEnumerable<User>> GetByRoleIdAsync(Guid roleId);
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsByEmailAsync(string email, Guid? excludeUserId = null);
    Task<bool> ExistsByDniAsync(string dni, Guid? excludeUserId = null);
    Task<int> CountAsync();
}
