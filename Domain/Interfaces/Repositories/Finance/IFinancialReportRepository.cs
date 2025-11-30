using Domain.Entities;

namespace Domain.Interfaces.Repositories.Finanzas
{
    public interface IFinancialReportRepository
    {
        Task<FinancialReport?> GetReportByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<bool> ExistsInPeriodAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<GeneralIncome>> GetIncomesAsync(Guid reportId);
        Task<IEnumerable<GeneralExpense>> GetExpensesAsync(Guid reportId);
        Task UpdateTotalsAsync(Guid reportId, decimal income, decimal expense, decimal netProfit);

        Task AddAsync(FinancialReport entity);
        void Update(FinancialReport entity);
        void Delete(FinancialReport entity);
        Task<bool> SaveChangesAsync();
    }
}
