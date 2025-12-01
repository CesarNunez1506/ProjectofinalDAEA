using Domain.Entities;

namespace Domain.Repositories.Sales
{
    public interface IStoreRepository
    {
        Task<Store?> GetByIdAsync(Guid id);
        Task<IEnumerable<Store>> GetAllAsync();
        Task AddAsync(Store store);
        Task UpdateAsync(Store store);
        Task DeleteAsync(Guid id);
    }
}
