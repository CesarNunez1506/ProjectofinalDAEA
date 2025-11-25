using Domain.Interfaces.Repositories.Inventory;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Inventory;

public class BuysProductRepository : GenericRepository<BuysProduct>, IBuysProductRepository
{
    public BuysProductRepository(LocalDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<BuysProduct>> GetBySupplierIdAsync(Guid supplierId)
    {
        return await _dbSet
            .Include(bp => bp.Supplier)
            .Include(bp => bp.Warehouse)
            .Where(bp => bp.SupplierId == supplierId)
            .OrderByDescending(bp => bp.PurchaseDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<BuysProduct>> GetByWarehouseIdAsync(Guid warehouseId)
    {
        return await _dbSet
            .Include(bp => bp.Supplier)
            .Include(bp => bp.Warehouse)
            .Where(bp => bp.WarehouseId == warehouseId)
            .OrderByDescending(bp => bp.PurchaseDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<BuysProduct>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Include(bp => bp.Supplier)
            .Include(bp => bp.Warehouse)
            .Where(bp => bp.PurchaseDate >= startDate && bp.PurchaseDate <= endDate)
            .OrderByDescending(bp => bp.PurchaseDate)
            .ToListAsync();
    }

    public async Task<BuysProduct?> GetPurchaseWithDetailsAsync(Guid purchaseId)
    {
        return await _dbSet
            .Include(bp => bp.Supplier)
            .Include(bp => bp.Warehouse)
            .Include(bp => bp.ProductPurchaseds)
                .ThenInclude(pp => pp.Product)
            .FirstOrDefaultAsync(bp => bp.Id == purchaseId);
    }

    public async Task<IEnumerable<BuysProduct>> GetActivePurchasesAsync()
    {
        return await _dbSet
            .Include(bp => bp.Supplier)
            .Include(bp => bp.Warehouse)
            .Where(bp => bp.Status == true)
            .OrderByDescending(bp => bp.PurchaseDate)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalPurchasesBySupplierAsync(Guid supplierId, DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Where(bp => bp.SupplierId == supplierId
                && bp.PurchaseDate >= startDate
                && bp.PurchaseDate <= endDate
                && bp.Status == true)
            .SumAsync(bp => bp.TotalAmount);
    }

    public async Task<IEnumerable<BuysProduct>> GetRecentPurchasesAsync(int count = 10)
    {
        return await _dbSet
            .Include(bp => bp.Supplier)
            .Include(bp => bp.Warehouse)
            .OrderByDescending(bp => bp.PurchaseDate)
            .Take(count)
            .ToListAsync();
    }
}
