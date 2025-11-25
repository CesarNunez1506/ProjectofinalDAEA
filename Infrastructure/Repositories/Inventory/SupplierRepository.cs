using Domain.Interfaces.Repositories.Inventory;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Inventory;

public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
{
    public SupplierRepository(LocalDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Supplier>> GetActiveSuppliersAsync()
    {
        return await _dbSet
            .Where(s => s.Status == true)
            .OrderBy(s => s.SuplierName)
            .ToListAsync();
    }

    public async Task<Supplier?> GetByRucAsync(long ruc)
    {
        return await _dbSet
            .FirstOrDefaultAsync(s => s.Ruc == ruc);
    }

    public async Task<IEnumerable<Supplier>> SearchByNameAsync(string name)
    {
        return await _dbSet
            .Where(s => s.SuplierName.Contains(name))
            .ToListAsync();
    }

    public async Task<Supplier?> GetByEmailAsync(string email)
    {
        return await _dbSet
            .FirstOrDefaultAsync(s => s.Email == email);
    }

    public async Task<Supplier?> GetSupplierWithResourcesAsync(Guid supplierId)
    {
        return await _dbSet
            .Include(s => s.Resources)
            .FirstOrDefaultAsync(s => s.Id == supplierId);
    }

    public async Task<Supplier?> GetSupplierWithPurchasesAsync(Guid supplierId)
    {
        return await _dbSet
            .Include(s => s.BuysProducts)
            .FirstOrDefaultAsync(s => s.Id == supplierId);
    }

    public async Task<bool> ExistsByRucAsync(long ruc)
    {
        return await _dbSet.AnyAsync(s => s.Ruc == ruc);
    }
}
