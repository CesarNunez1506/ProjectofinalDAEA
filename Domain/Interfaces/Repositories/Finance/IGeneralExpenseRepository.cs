using Domain.Entities;

namespace Domain.Interfaces.Repositories.Finanzas
{
    public interface IGeneralExpenseRepository
    {
        Task<IEnumerable<GeneralExpense>> GetExpensesByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<GeneralExpense>> GetByReportIdAsync(Guid reportId);
        Task<IEnumerable<GeneralExpense>> GetByModuleAsync(Guid moduleId);
        Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate);

        Task AddAsync(GeneralExpense entity);
        void Update(GeneralExpense entity);
        void Delete(GeneralExpense entity);
        Task<bool> SaveChangesAsync();
    }
}
