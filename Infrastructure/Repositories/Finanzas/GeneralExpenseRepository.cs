using Domain.Entities;
using Domain.Interfaces.Repositories.Finanzas;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
            return await _context.GeneralExpenses
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .ToListAsync();
        }

        /// <summary>
        /// Obtener gastos asociados a un reporte financiero específico
        /// </summary>
        public async Task<IEnumerable<GeneralExpense>> GetByReportIdAsync(Guid reportId)
        {
            return await _context.GeneralExpenses
                .Where(e => e.ReportId == reportId)
                .ToListAsync();
        }

        /// <summary>
        /// Obtener gastos por módulo (ejemplo: cocina, almacén, producción)
        /// </summary>
        public async Task<IEnumerable<GeneralExpense>> GetByModuleAsync(Guid moduleId)
        {
            return await _context.GeneralExpenses
                .Where(e => e.ModuleId == moduleId)
                .ToListAsync();
        }

        /// <summary>
        /// Sumar el total de gastos en un periodo (para cálculos de reportes)
        /// </summary>
        public async Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.GeneralExpenses
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .SumAsync(e => e.Amount);
        }

        public void Delete(GeneralExpense entity)
        {
            _context.GeneralExpenses.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
