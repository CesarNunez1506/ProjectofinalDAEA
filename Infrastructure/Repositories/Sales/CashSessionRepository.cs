using Domain.Entities;
using Domain.Repositories.Sales;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Sales
{
    public class CashSessionRepository : ICashSessionRepository
    {
        private readonly AppDbContext _context;

        public CashSessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CashSession>> GetAllAsync()
        {
            return await _context.CashSessions.ToListAsync();
        }

        public async Task<CashSession?> GetByIdAsync(Guid id)
        {
            return await _context.CashSessions.FindAsync(id);
        }

        public async Task<CashSession?> GetOpenSessionByStoreAsync(Guid storeId)
        {
            return await _context.CashSessions
                .Where(c => c.StoreId == storeId && c.ClosedAt == null)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(CashSession session)
        {
            await _context.CashSessions.AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CashSession session)
        {
            _context.CashSessions.Update(session);
            await _context.SaveChangesAsync();
        }
    }
}
