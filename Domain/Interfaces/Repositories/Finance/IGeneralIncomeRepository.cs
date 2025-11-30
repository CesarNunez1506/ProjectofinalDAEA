using Domain.Entities;

namespace Domain.Interfaces.Repositories.Finanzas
{
    public interface IGeneralIncomeRepository
    {
        Task<IEnumerable<GeneralIncome>> GetIncomesByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<GeneralIncome>> GetByReportIdAsync(Guid reportId);
        Task<IEnumerable<GeneralIncome>> GetByModuleAsync(Guid moduleId);
        Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate);

        Task AddAsync(GeneralIncome entity);
        void Update(GeneralIncome entity);
        void Delete(GeneralIncome entity);
        Task<bool> SaveChangesAsync();
    }
}
