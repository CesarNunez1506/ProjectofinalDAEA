using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Repositories.Finance;

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
            return await _dbSet
                .Where(i => i.Date >= startDate && i.Date <= endDate)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtener ingresos asociados a un reporte financiero específico
        /// </summary>
        public async Task<IEnumerable<GeneralIncome>> GetByReportIdAsync(Guid reportId)
        {
            return await _dbSet
                .Where(i => i.ReportId == reportId)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtener ingresos por módulo (ejemplo: cocina, almacén, producción)
        /// </summary>
        public async Task<IEnumerable<GeneralIncome>> GetByModuleAsync(Guid moduleId)
        {
            return await _dbSet
                .Where(i => i.ModuleId == moduleId)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtener el total de ingresos en un periodo (para cálculos de reportes)
        /// </summary>
        public async Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(i => i.Date >= startDate && i.Date <= endDate)
                .SumAsync(i => i.Amount);
        }

        /// <summary>
        /// Eliminar ingreso
        /// </summary>
        public void DeleteIncome(GeneralIncome entity)
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
