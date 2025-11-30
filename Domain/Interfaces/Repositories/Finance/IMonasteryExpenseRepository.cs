using Domain.Entities;

namespace Domain.Interfaces.Repositories.Finance
{
    /// <summary>
    /// Repositorio específico para MonasteryExpense
    /// Hereda de IRepository para operaciones genéricas
    /// </summary>
    public interface IMonasteryExpenseRepository : IRepository<MonasteryExpense>
    {
        // Métodos específicos del repositorio
        Task<IEnumerable<MonasteryExpense>> GetExpensesByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<MonasteryExpense>> GetByCategoryAsync(string category);
        Task<IEnumerable<MonasteryExpense>> GetByOverheadIdAsync(Guid overheadId);
        Task<double> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
