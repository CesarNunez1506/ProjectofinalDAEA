using Domain.Entities;
using Domain.Interfaces.Repositories.Finanzas;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FinancialReportRepository 
        : GenericRepository<FinancialReport>, IFinancialReportRepository
    {
        private readonly AppDbContext _context;

        public FinancialReportRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtener reporte por rango de fechas
        /// </summary>
        public async Task<FinancialReport?> GetReportByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.FinancialReports
                .Include(r => r.GeneralIncomes)
                .Include(r => r.GeneralExpenses)
                .FirstOrDefaultAsync(r =>
                    r.StartDate >= startDate &&
                    r.EndDate <= endDate);
        }

        /// <summary>
        /// Validar si ya existe un reporte en ese periodo
        /// </summary>
        public async Task<bool> ExistsInPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.FinancialReports
                .AnyAsync(r => r.StartDate == startDate && r.EndDate == endDate);
        }

        /// <summary>
        /// Obtener ingresos asociados a un reporte
        /// </summary>
        public async Task<IEnumerable<GeneralIncome>> GetIncomesAsync(Guid reportId)
        {
            return await _context.GeneralIncomes
                .Where(g => g.ReportId == reportId)
                .ToListAsync();
        }

        /// <summary>
        /// Obtener gastos asociados a un reporte
        /// </summary>
        public async Task<IEnumerable<GeneralExpense>> GetExpensesAsync(Guid reportId)
        {
            return await _context.GeneralExpenses
                .Where(e => e.ReportId == reportId)
                .ToListAsync();
        }

        /// <summary>
        /// Actualizar totales de un reporte financiero
        /// </summary>
        public async Task UpdateTotalsAsync(Guid reportId, decimal income, decimal expense, decimal netProfit)
        {
            var report = await _context.FinancialReports.FindAsync(reportId);

            if (report == null)
                return;

            report.TotalIncome = income;
            report.TotalExpenses = expense;
            report.NetProfit = netProfit;
            report.UpdatedAt = DateTime.UtcNow;

            // Se puede usar base.Update si lo quieres pasar al GenericRepository
            _context.FinancialReports.Update(report);
            await _context.SaveChangesAsync();
        }

        public void Delete(FinancialReport entity)
        {
            _context.FinancialReports.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
