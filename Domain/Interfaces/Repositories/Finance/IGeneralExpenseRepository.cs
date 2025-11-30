using Domain.Entities;

namespace Domain.Interfaces.Repositories.Finance
{
    /// <summary>
    /// Repositorio específico para GeneralExpense
    /// Hereda de IRepository para operaciones genéricas
    /// </summary>
    public interface IGeneralExpenseRepository : IRepository<GeneralExpense>
    {
        // Métodos específicos del repositorio
        Task<IEnumerable<GeneralExpense>> GetExpensesByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<GeneralExpense>> GetByReportIdAsync(Guid reportId);
        Task<IEnumerable<GeneralExpense>> GetByModuleAsync(Guid moduleId);
        Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
