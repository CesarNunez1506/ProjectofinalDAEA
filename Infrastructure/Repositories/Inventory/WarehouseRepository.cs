using Domain.Interfaces.Repositories.Inventory;
using Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Inventory;

public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(LocalDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Warehouse>> GetActiveWarehousesAsync()
    {
        return await _dbSet
            .Where(w => w.Status == true)
            .OrderBy(w => w.Name)
            .ToListAsync();
    }

    public async Task<Warehouse?> GetWarehouseWithProductsAsync(Guid warehouseId)
    {
        return await _dbSet
            .Include(w => w.WarehouseProducts)
                .ThenInclude(wp => wp.Product)
            .FirstOrDefaultAsync(w => w.Id == warehouseId);
    }

    public async Task<Warehouse?> GetWarehouseWithResourcesAsync(Guid warehouseId)
    {
        return await _dbSet
            .Include(w => w.WarehouseResources)
                .ThenInclude(wr => wr.Resource)
            .FirstOrDefaultAsync(w => w.Id == warehouseId);
    }

    public async Task<int> GetProductStockAsync(Guid warehouseId, Guid productId)
    {
        var warehouseProduct = await _context.WarehouseProducts
            .FirstOrDefaultAsync(wp => wp.WarehouseId == warehouseId && wp.ProductId == productId);

        return warehouseProduct?.Quantity ?? 0;
    }

    public async Task<int> GetResourceStockAsync(Guid warehouseId, Guid resourceId)
    {
        var warehouseResource = await _context.WarehouseResources
            .FirstOrDefaultAsync(wr => wr.WarehouseId == warehouseId && wr.ResourceId == resourceId);

        return warehouseResource?.Quantity ?? 0;
    }

    public async Task<IEnumerable<Warehouse>> SearchByLocationAsync(string location)
    {
        return await _dbSet
            .Where(w => w.Location.Contains(location))
            .ToListAsync();
    }

    public async Task<bool> HasCapacityAsync(Guid warehouseId)
    {
        var warehouse = await _dbSet
            .Include(w => w.WarehouseProducts)
            .FirstOrDefaultAsync(w => w.Id == warehouseId);

        if (warehouse == null) return false;

        var currentLoad = warehouse.WarehouseProducts.Sum(wp => wp.Quantity);
        return currentLoad < warehouse.Capacity;
    }
}
