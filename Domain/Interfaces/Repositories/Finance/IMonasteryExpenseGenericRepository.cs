using Domain.Entities;

namespace Domain.Interfaces.Repositories.Finance
{
    /// <summary>
    /// Repositorio genérico para MonasteryExpense (contrato)
    /// </summary>
    public interface IMonasteryExpenseGenericRepository : IRepository<MonasteryExpense>
    {
        Task<IEnumerable<MonasteryExpense>> GetExpensesByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<MonasteryExpense>> GetByCategoryAsync(string category);
        Task<IEnumerable<MonasteryExpense>> GetByOverheadIdAsync(Guid overheadId);
        Task<double> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
