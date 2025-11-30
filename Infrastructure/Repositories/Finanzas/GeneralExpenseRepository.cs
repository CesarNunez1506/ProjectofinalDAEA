using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Repositories.Finance;

namespace Infrastructure.Repositories
{
    public class GeneralExpenseRepository 
        : GenericRepository<GeneralExpense>, IGeneralExpenseRepository
    {
        private readonly AppDbContext _context;

        public GeneralExpenseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtener todos los gastos dentro de un periodo
        /// </summary>
        public async Task<IEnumerable<GeneralExpense>> GetExpensesByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtener gastos asociados a un reporte financiero específico
        /// </summary>
        public async Task<IEnumerable<GeneralExpense>> GetByReportIdAsync(Guid reportId)
        {
            return await _dbSet
                .Where(e => e.ReportId == reportId)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtener gastos por módulo (ejemplo: cocina, almacén, producción)
        /// </summary>
        public async Task<IEnumerable<GeneralExpense>> GetByModuleAsync(Guid moduleId)
        {
            return await _dbSet
                .Where(e => e.ModuleId == moduleId)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Sumar el total de gastos en un periodo (para cálculos de reportes)
        /// </summary>
        public async Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .SumAsync(e => e.Amount);
        }

        /// <summary>
        /// Eliminar gasto
        /// </summary>
        public void DeleteExpense(GeneralExpense entity)
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
