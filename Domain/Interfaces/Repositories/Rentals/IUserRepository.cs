using Domain.Entities;

namespace Domain.Interfaces.Repositories.Rentals;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
}
