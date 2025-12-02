using Domain.Interfaces.Repositories.Inventory;
using Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Inventory;

public class ResourceRepository : GenericRepository<Resource>, IResourceRepository
{
    public ResourceRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Resource>> GetActiveResourcesAsync()
    {
        return await _dbSet
            .Include(r => r.Supplier)
            .Where(r => r.Status == true)
            .ToListAsync();
    }

    public async Task<IEnumerable<Resource>> GetBySupplierIdAsync(Guid supplierId)
    {
        return await _dbSet
            .Include(r => r.Supplier)
            .Where(r => r.SupplierId == supplierId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Resource>> SearchByNameAsync(string name)
    {
        return await _dbSet
            .Include(r => r.Supplier)
            .Where(r => r.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<Resource?> GetResourceWithSupplierAsync(Guid resourceId)
    {
        return await _dbSet
            .Include(r => r.Supplier)
            .FirstOrDefaultAsync(r => r.Id == resourceId);
    }

    public async Task<IEnumerable<Resource>> GetLowStockResourcesAsync(int minQuantity = 10)
    {
        return await _dbSet
            .Include(r => r.Supplier)
            .Include(r => r.WarehouseResources)
            .Where(r => r.WarehouseResources.Sum(wr => wr.Quantity) < minQuantity)
            .ToListAsync();
    }

    public async Task<IEnumerable<Resource>> GetResourcesUsedInRecipesAsync()
    {
        return await _dbSet
            .Include(r => r.Supplier)
            .Include(r => r.Recipes)
            .Where(r => r.Recipes.Any())
            .ToListAsync();
    }
}
