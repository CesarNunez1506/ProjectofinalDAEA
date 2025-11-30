using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Repositories.Finance;

namespace Infrastructure.Repositories
{
    public class OverheadRepository 
        : GenericRepository<Overhead>, IOverheadRepository
    {
        private readonly AppDbContext _context;

        public OverheadRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtener Overheads por tipo (ej: mantenimiento, servicios, etc.)
        /// </summary>
        public async Task<IEnumerable<Overhead>> GetByTypeAsync(string type)
        {
            return await _dbSet
                .Where(o => o.Type == type)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtener Overheads por estado (activo/inactivo)
        /// </summary>
        public async Task<IEnumerable<Overhead>> GetByStatusAsync(bool status)
        {
            return await _dbSet
                .Where(o => o.Status == status)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtener Overheads dentro de un rango de fechas
        /// </summary>
        public async Task<IEnumerable<Overhead>> GetByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(o => o.Date >= startDate && o.Date <= endDate)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtener el total de Overheads dentro de un periodo
        /// </summary>
        public async Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(o => o.Date >= startDate && o.Date <= endDate)
                .SumAsync(o => o.Amount);
        }

        /// <summary>
        /// Obtener un Overhead con sus gastos relacionados
        /// </summary>
        public async Task<Overhead?> GetWithExpensesAsync(Guid overheadId)
        {
            return await _dbSet
                .Include(o => o.MonasteryExpenses)
                .FirstOrDefaultAsync(o => o.Id == overheadId);
        }

        /// <summary>
        /// Eliminar overhead
        /// </summary>
        public void DeleteOverhead(Overhead entity)
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
