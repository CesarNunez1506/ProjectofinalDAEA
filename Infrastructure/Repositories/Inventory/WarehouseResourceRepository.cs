using Domain.Interfaces.Repositories.Inventory;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Inventory;

public class WarehouseResourceRepository : GenericRepository<WarehouseResource>, IWarehouseResourceRepository
{
    public WarehouseResourceRepository(LocalDbContext context) : base(context)
    {
    }

    public async Task<WarehouseResource?> GetStockAsync(Guid warehouseId, Guid resourceId)
    {
        return await _dbSet
            .Include(wr => wr.Warehouse)
            .Include(wr => wr.Resource)
            .FirstOrDefaultAsync(wr => wr.WarehouseId == warehouseId && wr.ResourceId == resourceId);
    }

    public async Task<IEnumerable<WarehouseResource>> GetByWarehouseIdAsync(Guid warehouseId)
    {
        return await _dbSet
            .Include(wr => wr.Resource)
                .ThenInclude(r => r.Supplier)
            .Where(wr => wr.WarehouseId == warehouseId)
            .ToListAsync();
    }

    public async Task<IEnumerable<WarehouseResource>> GetByResourceIdAsync(Guid resourceId)
    {
        return await _dbSet
            .Include(wr => wr.Warehouse)
            .Where(wr => wr.ResourceId == resourceId)
            .ToListAsync();
    }

    public async Task<IEnumerable<WarehouseResource>> GetActiveStockAsync()
    {
        return await _dbSet
            .Include(wr => wr.Warehouse)
            .Include(wr => wr.Resource)
            .Where(wr => wr.Status == true && wr.Quantity > 0)
            .ToListAsync();
    }

    public async Task UpdateStockQuantityAsync(Guid warehouseId, Guid resourceId, int quantity)
    {
        var warehouseResource = await GetStockAsync(warehouseId, resourceId);

        if (warehouseResource != null)
        {
            warehouseResource.Quantity = quantity;
            warehouseResource.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(warehouseResource);
        }
    }

    public async Task<int> GetTotalStockByResourceAsync(Guid resourceId)
    {
        return await _dbSet
            .Where(wr => wr.ResourceId == resourceId && wr.Status == true)
            .SumAsync(wr => wr.Quantity);
    }

    public async Task<bool> HasSufficientStockAsync(Guid warehouseId, Guid resourceId, int requiredQuantity)
    {
        var warehouseResource = await GetStockAsync(warehouseId, resourceId);
        return warehouseResource != null && warehouseResource.Quantity >= requiredQuantity;
    }
}
