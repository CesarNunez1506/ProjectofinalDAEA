using Domain.Entities;

namespace Domain.Repositories.Sales
{
    public interface ICashSessionRepository
    {
        Task<IEnumerable<CashSession>> GetAllAsync();
        Task<CashSession?> GetByIdAsync(Guid id);
        Task<CashSession?> GetOpenSessionByStoreAsync(Guid storeId);
        Task AddAsync(CashSession session);
        Task UpdateAsync(CashSession session);
    }
}
