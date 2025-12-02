using Domain.Entities;

namespace Domain.Interfaces.Repositories.Finance
{
    /// <summary>
    /// Repositorio genérico para Overhead (contrato)
    /// </summary>
    public interface IOverheadGenericRepository : IRepository<Overhead>
    {
        Task<IEnumerable<Overhead>> GetByTypeAsync(string type);
        Task<IEnumerable<Overhead>> GetByStatusAsync(bool status);
        Task<IEnumerable<Overhead>> GetByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<Overhead?> GetWithExpensesAsync(Guid overheadId);
    }
}
