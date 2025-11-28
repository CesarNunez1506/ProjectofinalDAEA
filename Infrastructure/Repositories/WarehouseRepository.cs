using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>
/// Implementación del repositorio de almacén para operaciones de producción
/// </summary>
public class WarehouseRepository : IWarehouseRepository
{
    private readonly AppDbContext _context;

    public WarehouseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<WarehouseProduct?> GetWarehouseProductAsync(Guid warehouseId, Guid productId)
    {
        return await _context.WarehouseProducts
            .FirstOrDefaultAsync(wp => wp.WarehouseId == warehouseId && wp.ProductId == productId);
    }

    public async Task AddWarehouseProductAsync(WarehouseProduct warehouseProduct)
    {
        _context.WarehouseProducts.Add(warehouseProduct);
    }

    public async Task UpdateWarehouseProductAsync(WarehouseProduct warehouseProduct)
    {
        _context.WarehouseProducts.Update(warehouseProduct);
    }

    public async Task AddWarehouseMovementProductAsync(WarehouseMovementProduct movement)
    {
        _context.WarehouseMovementProducts.Add(movement);
    }

    public async Task<List<WarehouseResource>> GetWarehouseResourcesByResourceIdAsync(Guid warehouseId, Guid resourceId)
    {
        return await _context.WarehouseResources
            .Where(wr => wr.WarehouseId == warehouseId && wr.ResourceId == resourceId && wr.Quantity > 0)
            .OrderBy(wr => wr.EntryDate)
            .ToListAsync();
    }

    public async Task UpdateWarehouseResourceAsync(WarehouseResource warehouseResource)
    {
        _context.WarehouseResources.Update(warehouseResource);
    }

    public async Task AddWarehouseMovementResourceAsync(WarehouseMovementResource movement)
    {
        _context.WarehouseMovementResources.Add(movement);
    }

    public async Task<Resource?> GetResourceByIdAsync(Guid resourceId)
    {
        return await _context.Resources.FindAsync(resourceId);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
