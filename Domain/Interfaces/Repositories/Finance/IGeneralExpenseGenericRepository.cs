using Domain.Entities;

namespace Domain.Interfaces.Repositories.Finance
{
    /// <summary>
    /// Repositorio genérico para GeneralExpense (contrato)
    /// </summary>
    public interface IGeneralExpenseGenericRepository : IRepository<GeneralExpense>
    {
        Task<IEnumerable<GeneralExpense>> GetExpensesByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<GeneralExpense>> GetByReportIdAsync(Guid reportId);
        Task<IEnumerable<GeneralExpense>> GetByModuleAsync(Guid moduleId);
        Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
