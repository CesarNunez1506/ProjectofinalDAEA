using Domain.Entities;

namespace Domain.Repositories.Sales
{
    public interface ISalesChannelRepository
    {
        Task<IEnumerable<SalesChannel>> GetAllAsync();
        Task<SalesChannel?> GetByIdAsync(Guid id);
        Task AddAsync(SalesChannel channel);
        Task UpdateAsync(SalesChannel channel);
        Task DeleteAsync(Guid id);
    }
}
