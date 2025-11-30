using Domain.Entities;

namespace Domain.Interfaces.Repositories.Finanzas
{
    public interface IMonasteryExpenseRepository
    {
        Task<IEnumerable<MonasteryExpense>> GetExpensesByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<MonasteryExpense>> GetByCategoryAsync(string category);
        Task<IEnumerable<MonasteryExpense>> GetByOverheadIdAsync(Guid overheadId);
        Task<double> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate);

        Task AddAsync(MonasteryExpense entity);
        void Update(MonasteryExpense entity);
        void Delete(MonasteryExpense entity);
        Task<bool> SaveChangesAsync();
    }
}
