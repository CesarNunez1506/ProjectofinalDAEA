using Domain.Entities;

namespace Domain.Interfaces.Repositories.Finance
{
    /// <summary>
    /// Repositorio genérico para FinancialReport (contrato)
    /// </summary>
    public interface IFinancialReportGenericRepository : IRepository<FinancialReport>
    {
        Task<FinancialReport?> GetReportByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<GeneralIncome>> GetIncomesAsync(Guid reportId);
        Task<IEnumerable<GeneralExpense>> GetExpensesAsync(Guid reportId);
        Task UpdateTotalsAsync(Guid reportId, decimal income, decimal expense, decimal netProfit);
        Task<bool> ExistsInPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
