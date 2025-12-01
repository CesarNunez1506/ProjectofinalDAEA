using Domain.Entities;

namespace Domain.Repositories.Sales
{
    public interface ISaleDetailRepository
    {
        Task<SaleDetail?> GetByIdAsync(Guid id);
        Task<IEnumerable<SaleDetail>> GetBySaleIdAsync(Guid saleId);
        Task AddAsync(SaleDetail detail);
        Task UpdateAsync(SaleDetail detail);
        Task DeleteAsync(Guid id);
    }
}
