using Domain.Entities;

namespace Domain.Interfaces.Repositories.Finance
{
    /// <summary>
    /// Repositorio específico para FinancialReport
    /// Hereda de IRepository para operaciones genéricas
    /// </summary>
    public interface IFinancialReportRepository : IRepository<FinancialReport>
    {
        // Métodos específicos del repositorio
        Task<FinancialReport?> GetReportByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<GeneralIncome>> GetIncomesAsync(Guid reportId);
        Task<IEnumerable<GeneralExpense>> GetExpensesAsync(Guid reportId);
        Task UpdateTotalsAsync(Guid reportId, decimal income, decimal expense, decimal netProfit);
        Task<bool> ExistsInPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
