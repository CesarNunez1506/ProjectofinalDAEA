using Domain.Entities;

namespace Domain.Interfaces.Repositories.Finance
{
    /// <summary>
    /// Repositorio específico para GeneralIncome
    /// Hereda de IRepository para operaciones genéricas
    /// </summary>
    public interface IGeneralIncomeRepository : IRepository<GeneralIncome>
    {
        // Métodos específicos del repositorio
        Task<IEnumerable<GeneralIncome>> GetIncomesByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<GeneralIncome>> GetByReportIdAsync(Guid reportId);
        Task<IEnumerable<GeneralIncome>> GetByModuleAsync(Guid moduleId);
        Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
