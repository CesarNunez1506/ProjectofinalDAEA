using Domain.Interfaces.Repositories.Inventory;
using Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Inventory;

public class WarehouseMovementResourceRepository : GenericRepository<WarehouseMovementResource>, IWarehouseMovementResourceRepository
{
    public WarehouseMovementResourceRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<WarehouseMovementResource>> GetByWarehouseIdAsync(Guid warehouseId)
    {
        return await _dbSet
            .Include(wm => wm.Warehouse)
            .Include(wm => wm.Resource)
            .Where(wm => wm.WarehouseId == warehouseId)
            .OrderByDescending(wm => wm.MovementDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<WarehouseMovementResource>> GetByResourceIdAsync(Guid resourceId)
    {
        return await _dbSet
            .Include(wm => wm.Warehouse)
            .Include(wm => wm.Resource)
            .Where(wm => wm.ResourceId == resourceId)
            .OrderByDescending(wm => wm.MovementDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<WarehouseMovementResource>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Include(wm => wm.Warehouse)
            .Include(wm => wm.Resource)
            .Where(wm => wm.MovementDate >= startDate && wm.MovementDate <= endDate)
            .OrderByDescending(wm => wm.MovementDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<WarehouseMovementResource>> GetByMovementTypeAsync(string movementType)
    {
        return await _dbSet
            .Include(wm => wm.Warehouse)
            .Include(wm => wm.Resource)
            .Where(wm => wm.MovementType == movementType)
            .OrderByDescending(wm => wm.MovementDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<WarehouseMovementResource>> GetResourceHistoryInWarehouseAsync(Guid warehouseId, Guid resourceId)
    {
        return await _dbSet
            .Include(wm => wm.Warehouse)
            .Include(wm => wm.Resource)
            .Where(wm => wm.WarehouseId == warehouseId && wm.ResourceId == resourceId)
            .OrderByDescending(wm => wm.MovementDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<WarehouseMovementResource>> GetRecentMovementsAsync(int count = 20)
    {
        return await _dbSet
            .Include(wm => wm.Warehouse)
            .Include(wm => wm.Resource)
            .OrderByDescending(wm => wm.MovementDate)
            .Take(count)
            .ToListAsync();
    }
}
