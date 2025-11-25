using Domain.Interfaces.Repositories.Inventory;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Inventory;

public class WarehouseProductRepository : GenericRepository<WarehouseProduct>, IWarehouseProductRepository
{
    public WarehouseProductRepository(LocalDbContext context) : base(context)
    {
    }

    public async Task<WarehouseProduct?> GetStockAsync(Guid warehouseId, Guid productId)
    {
        return await _dbSet
            .Include(wp => wp.Warehouse)
            .Include(wp => wp.Product)
            .FirstOrDefaultAsync(wp => wp.WarehouseId == warehouseId && wp.ProductId == productId);
    }

    public async Task<IEnumerable<WarehouseProduct>> GetByWarehouseIdAsync(Guid warehouseId)
    {
        return await _dbSet
            .Include(wp => wp.Product)
                .ThenInclude(p => p.Category)
            .Where(wp => wp.WarehouseId == warehouseId)
            .ToListAsync();
    }

    public async Task<IEnumerable<WarehouseProduct>> GetByProductIdAsync(Guid productId)
    {
        return await _dbSet
            .Include(wp => wp.Warehouse)
            .Where(wp => wp.ProductId == productId)
            .ToListAsync();
    }

    public async Task<IEnumerable<WarehouseProduct>> GetActiveStockAsync()
    {
        return await _dbSet
            .Include(wp => wp.Warehouse)
            .Include(wp => wp.Product)
            .Where(wp => wp.Status == true && wp.Quantity > 0)
            .ToListAsync();
    }

    public async Task UpdateStockQuantityAsync(Guid warehouseId, Guid productId, int quantity)
    {
        var warehouseProduct = await GetStockAsync(warehouseId, productId);

        if (warehouseProduct != null)
        {
            warehouseProduct.Quantity = quantity;
            warehouseProduct.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(warehouseProduct);
        }
    }

    public async Task<int> GetTotalStockByProductAsync(Guid productId)
    {
        return await _dbSet
            .Where(wp => wp.ProductId == productId && wp.Status == true)
            .SumAsync(wp => wp.Quantity);
    }

    public async Task<bool> HasSufficientStockAsync(Guid warehouseId, Guid productId, int requiredQuantity)
    {
        var warehouseProduct = await GetStockAsync(warehouseId, productId);
        return warehouseProduct != null && warehouseProduct.Quantity >= requiredQuantity;
    }
}
