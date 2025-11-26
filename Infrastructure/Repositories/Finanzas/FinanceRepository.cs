using System;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class FinanceRepository : GenericRepository<FinancialReport>, IFinancialReportRepository
    {
        private readonly AppDbContext _ctx;
        public FinanceRepository(AppDbContext context) : base(context)
        {
            _ctx = context;
        }

        public async Task<FinancialReport> GenerateReportAsync(DateTime start, DateTime? end = null)
        {
            var endDate = end ?? DateTime.UtcNow;

            var totalIncome = await _ctx.GeneralIncomes
                .Where(i => i.Date >= start && i.Date <= endDate)
                .SumAsync(i => (decimal?)i.Amount) ?? 0m;

            var totalExpenses = await _ctx.GeneralExpenses
                .Where(e => e.Date >= start && e.Date <= endDate)
                .SumAsync(e => (decimal?)e.Amount) ?? 0m;

            var report = new FinancialReport
            {
                Id = Guid.NewGuid(),
                StartDate = start,
                EndDate = endDate,
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                NetProfit = totalIncome - totalExpenses,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _ctx.FinancialReports.AddAsync(report);
            await _ctx.SaveChangesAsync();
            return report;
        }
    }
}
