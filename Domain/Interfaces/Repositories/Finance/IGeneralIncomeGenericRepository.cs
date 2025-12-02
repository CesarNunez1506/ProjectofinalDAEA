using Domain.Entities;

namespace Domain.Interfaces.Repositories.Finance
{
    /// <summary>
    /// Repositorio genérico para GeneralIncome (contrato)
    /// </summary>
    public interface IGeneralIncomeGenericRepository : IRepository<GeneralIncome>
    {
        Task<IEnumerable<GeneralIncome>> GetIncomesByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<GeneralIncome>> GetByReportIdAsync(Guid reportId);
        Task<IEnumerable<GeneralIncome>> GetByModuleAsync(Guid moduleId);
        Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
