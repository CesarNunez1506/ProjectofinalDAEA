using Domain.Interfaces.Repositories.Inventory;
using Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Inventory;

public class BuysProductRepository : GenericRepository<BuysProduct>, IBuysProductRepository
{
    public BuysProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<BuysProduct>> GetBySupplierIdAsync(Guid supplierId)
    {
        return await _dbSet
            .Include(bp => bp.Supplier)
            .Include(bp => bp.Warehouse)
            .Where(bp => bp.SupplierId == supplierId)
            .OrderByDescending(bp => bp.EntryDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<BuysProduct>> GetByWarehouseIdAsync(Guid warehouseId)
    {
        return await _dbSet
            .Include(bp => bp.Supplier)
            .Include(bp => bp.Warehouse)
            .Where(bp => bp.WarehouseId == warehouseId)
            .OrderByDescending(bp => bp.EntryDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<BuysProduct>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Include(bp => bp.Supplier)
            .Include(bp => bp.Warehouse)
            .Where(bp => bp.EntryDate >= startDate && bp.EntryDate <= endDate)
            .OrderByDescending(bp => bp.EntryDate)
            .ToListAsync();
    }

    public async Task<BuysProduct?> GetPurchaseWithDetailsAsync(Guid purchaseId)
    {
        return await _dbSet
            .Include(bp => bp.Supplier)
            .Include(bp => bp.Warehouse)
            .Include(bp => bp.Product)
            .FirstOrDefaultAsync(bp => bp.Id == purchaseId);
    }

    public async Task<IEnumerable<BuysProduct>> GetActivePurchasesAsync()
    {
        return await _dbSet
            .Include(bp => bp.Supplier)
            .Include(bp => bp.Warehouse)
            .Where(bp => bp.Status == true)
            .OrderByDescending(bp => bp.EntryDate)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalPurchasesBySupplierAsync(Guid supplierId, DateTime startDate, DateTime endDate)
    {
        return (decimal)await _dbSet
            .Where(bp => bp.SupplierId == supplierId
                && bp.EntryDate >= startDate
                && bp.EntryDate <= endDate
                && bp.Status == true)
            .SumAsync(bp => bp.TotalCost);
    }

    public async Task<IEnumerable<BuysProduct>> GetRecentPurchasesAsync(int count = 10)
    {
        return await _dbSet
            .Include(bp => bp.Supplier)
            .Include(bp => bp.Warehouse)
            .OrderByDescending(bp => bp.EntryDate)
            .Take(count)
            .ToListAsync();
    }
}
