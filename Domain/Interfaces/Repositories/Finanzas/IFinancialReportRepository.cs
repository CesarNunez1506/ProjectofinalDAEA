using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IFinancialReportRepository : IGenericRepository<FinancialReport>
    {
        Task<FinancialReport> GenerateReportAsync(DateTime start, DateTime? end = null);
    }
}
