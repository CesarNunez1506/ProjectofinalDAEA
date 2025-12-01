using Domain.Interfaces.Repositories.Inventory;
using Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Inventory;

public class WarehouseMovementProductRepository : GenericRepository<WarehouseMovementProduct>, IWarehouseMovementProductRepository
{
    public WarehouseMovementProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<WarehouseMovementProduct>> GetByWarehouseIdAsync(Guid warehouseId)
    {
        return await _dbSet
            .Include(wm => wm.Warehouse)
            .Include(wm => wm.Product)
            .Where(wm => wm.WarehouseId == warehouseId)
            .OrderByDescending(wm => wm.MovementDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<WarehouseMovementProduct>> GetByProductIdAsync(Guid productId)
    {
        return await _dbSet
            .Include(wm => wm.Warehouse)
            .Include(wm => wm.Product)
            .Where(wm => wm.ProductId == productId)
            .OrderByDescending(wm => wm.MovementDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<WarehouseMovementProduct>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Include(wm => wm.Warehouse)
            .Include(wm => wm.Product)
            .Where(wm => wm.MovementDate >= startDate && wm.MovementDate <= endDate)
            .OrderByDescending(wm => wm.MovementDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<WarehouseMovementProduct>> GetByMovementTypeAsync(string movementType)
    {
        return await _dbSet
            .Include(wm => wm.Warehouse)
            .Include(wm => wm.Product)
            .Where(wm => wm.MovementType == movementType)
            .OrderByDescending(wm => wm.MovementDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<WarehouseMovementProduct>> GetProductHistoryInWarehouseAsync(Guid warehouseId, Guid productId)
    {
        return await _dbSet
            .Include(wm => wm.Warehouse)
            .Include(wm => wm.Product)
            .Where(wm => wm.WarehouseId == warehouseId && wm.ProductId == productId)
            .OrderByDescending(wm => wm.MovementDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<WarehouseMovementProduct>> GetRecentMovementsAsync(int count = 20)
    {
        return await _dbSet
            .Include(wm => wm.Warehouse)
            .Include(wm => wm.Product)
            .OrderByDescending(wm => wm.MovementDate)
            .Take(count)
            .ToListAsync();
    }
}
