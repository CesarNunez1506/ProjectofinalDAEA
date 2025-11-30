using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MonasteryExpenseRepository 
        : GenericRepository<MonasteryExpense>
    {
        private readonly AppDbContext _context;

        public MonasteryExpenseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Listar todos los gastos dentro de un periodo
        /// </summary>
        public async Task<IEnumerable<MonasteryExpense>> GetExpensesByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtener gastos por categoría
        /// </summary>
        public async Task<IEnumerable<MonasteryExpense>> GetByCategoryAsync(string category)
        {
            return await _dbSet
                .Where(e => e.Category == category)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtener gastos asociados a un Overhead específico
        /// </summary>
        public async Task<IEnumerable<MonasteryExpense>> GetByOverheadIdAsync(Guid overheadId)
        {
            return await _dbSet
                .Where(e => e.OverheadsId == overheadId)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtener el total de gastos en un periodo (para cálculos de reportes)
        /// </summary>
        public async Task<double> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .SumAsync(e => e.Amount);
        }

        /// <summary>
        /// Eliminar gasto
        /// </summary>
        public void DeleteExpense(MonasteryExpense entity)
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
