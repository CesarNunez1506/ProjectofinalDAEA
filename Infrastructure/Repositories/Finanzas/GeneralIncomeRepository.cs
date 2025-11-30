using Domain.Entities;
using Domain.Interfaces.Repositories.Finanzas;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GeneralIncomeRepository : GenericRepository<GeneralIncome>, IGeneralIncomeRepository
    {
        private readonly AppDbContext _context;

        public GeneralIncomeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtener todos los ingresos dentro de un periodo
        /// </summary>
        public async Task<IEnumerable<GeneralIncome>> GetIncomesByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.GeneralIncomes
                .Where(i => i.Date >= startDate && i.Date <= endDate)
                .ToListAsync();
        }

        /// <summary>
        /// Obtener ingresos asociados a un reporte financiero específico
        /// </summary>
        public async Task<IEnumerable<GeneralIncome>> GetByReportIdAsync(Guid reportId)
        {
            return await _context.GeneralIncomes
                .Where(i => i.ReportId == reportId)
                .ToListAsync();
        }

        /// <summary>
        /// Obtener ingresos por módulo (ejemplo: cocina, almacén, producción)
        /// </summary>
        public async Task<IEnumerable<GeneralIncome>> GetByModuleAsync(Guid moduleId)
        {
            return await _context.GeneralIncomes
                .Where(i => i.ModuleId == moduleId)
                .ToListAsync();
        }

        /// <summary>
        /// Obtener el total de ingresos en un periodo (para cálculos de reportes)
        /// </summary>
        public async Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.GeneralIncomes
                .Where(i => i.Date >= startDate && i.Date <= endDate)
                .SumAsync(i => i.Amount);
        }

        // Implement interface contract helpers
        public void Delete(GeneralIncome entity)
        {
            _context.GeneralIncomes.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }

}
