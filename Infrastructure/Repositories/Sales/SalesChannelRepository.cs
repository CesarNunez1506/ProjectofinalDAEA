using Domain.Entities;
using Domain.Repositories.Sales;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Sales
{
    public class SalesChannelRepository : ISalesChannelRepository
    {
        private readonly AppDbContext _context;

        public SalesChannelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalesChannel>> GetAllAsync()
        {
            return await _context.SalesChannels.ToListAsync();
        }

        public async Task<SalesChannel?> GetByIdAsync(Guid id)
        {
            return await _context.SalesChannels.FindAsync(id);
        }

        public async Task AddAsync(SalesChannel channel)
        {
            await _context.SalesChannels.AddAsync(channel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SalesChannel channel)
        {
            _context.SalesChannels.Update(channel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.SalesChannels.FindAsync(id);
            if (entity != null)
            {
                _context.SalesChannels.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
