using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Repositories.Finance;

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
            return await _dbSet
                .Include(r => r.GeneralIncomes)
                .Include(r => r.GeneralExpenses)
                .FirstOrDefaultAsync(r => r.StartDate >= startDate && r.EndDate <= endDate);
        }

        /// <summary>
        /// Validar si ya existe un reporte en ese periodo
        /// </summary>
        public async Task<bool> ExistsInPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await AnyAsync(r => r.StartDate == startDate && r.EndDate == endDate);
        }

        /// <summary>
        /// Obtener ingresos asociados a un reporte
        /// </summary>
        public async Task<IEnumerable<GeneralIncome>> GetIncomesAsync(Guid reportId)
        {
            return await _context.GeneralIncomes
                .Where(g => g.ReportId == reportId)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtener gastos asociados a un reporte
        /// </summary>
        public async Task<IEnumerable<GeneralExpense>> GetExpensesAsync(Guid reportId)
        {
            return await _context.GeneralExpenses
                .Where(e => e.ReportId == reportId)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Actualizar totales de un reporte financiero
        /// </summary>
        public async Task UpdateTotalsAsync(Guid reportId, decimal income, decimal expense, decimal netProfit)
        {
            var report = await GetByIdAsync(reportId);

            if (report == null) return;

            report.TotalIncome = income;
            report.TotalExpenses = expense;
            report.NetProfit = netProfit;
            report.UpdatedAt = DateTime.UtcNow;

            Update(report);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Eliminar reporte financiero
        /// </summary>
        public void DeleteReport(FinancialReport entity)
        {
            Remove(entity);
        }

        /// <summary>
        /// Guardar cambios en la base de datos
        /// </summary>
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
