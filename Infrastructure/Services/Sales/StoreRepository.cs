using Domain.Entities;
using Domain.Repositories.Sales;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Sales
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AppDbContext _context;

        public StoreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Store?> GetByIdAsync(Guid id)
        {
            return await _context.Stores.FindAsync(id);
        }

        public async Task<IEnumerable<Store>> GetAllAsync()
        {
            return await _context.Stores.ToListAsync();
        }

        public async Task AddAsync(Store store)
        {
            await _context.Stores.AddAsync(store);
        }

        public async Task UpdateAsync(Store store)
        {
            _context.Stores.Update(store);
        }

        public async Task DeleteAsync(Guid id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
                _context.Stores.Remove(store);
        }
    }
}
