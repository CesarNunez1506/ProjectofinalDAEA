using Domain.Entities;
using Domain.Repositories.Sales;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Sales
{
    public class SaleDetailRepository : ISaleDetailRepository
    {
        private readonly AppDbContext _context;

        public SaleDetailRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SaleDetail?> GetByIdAsync(Guid id)
        {
            return await _context.SaleDetails.FindAsync(id);
        }

        public async Task<IEnumerable<SaleDetail>> GetBySaleIdAsync(Guid saleId)
        {
            return await _context.SaleDetails
                .Where(x => x.SaleId == saleId)
                .ToListAsync();
        }

        public async Task AddAsync(SaleDetail detail)
        {
            await _context.SaleDetails.AddAsync(detail);
        }

        public async Task UpdateAsync(SaleDetail detail)
        {
            _context.SaleDetails.Update(detail);
        }

        public async Task DeleteAsync(Guid id)
        {
            var detail = await _context.SaleDetails.FindAsync(id);
            if (detail != null)
                _context.SaleDetails.Remove(detail);
        }
    }
}
