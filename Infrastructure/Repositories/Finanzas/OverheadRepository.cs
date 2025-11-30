using Domain.Entities;
using Domain.Interfaces.Repositories.Finanzas;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Overheads
                .Where(o => o.Type == type)
                .ToListAsync();
        }

        /// <summary>
        /// Obtener Overheads por estado (activo/inactivo)
        /// </summary>
        public async Task<IEnumerable<Overhead>> GetByStatusAsync(bool status)
        {
            return await _context.Overheads
                .Where(o => o.Status == status)
                .ToListAsync();
        }

        /// <summary>
        /// Obtener Overheads dentro de un rango de fechas
        /// </summary>
        public async Task<IEnumerable<Overhead>> GetByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Overheads
                .Where(o => o.Date >= startDate && o.Date <= endDate)
                .ToListAsync();
        }

        /// <summary>
        /// Obtener el total de Overheads dentro de un periodo
        /// </summary>
        public async Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Overheads
                .Where(o => o.Date >= startDate && o.Date <= endDate)
                .SumAsync(o => o.Amount);
        }

        /// <summary>
        /// Obtener un Overhead con sus gastos relacionados
        /// </summary>
        public async Task<Overhead?> GetWithExpensesAsync(Guid overheadId)
        {
            return await _context.Overheads
                .Include(o => o.MonasteryExpenses)
                .FirstOrDefaultAsync(o => o.Id == overheadId);
        }

        public void Delete(Overhead entity)
        {
            _context.Overheads.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
