using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;


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

    public Task AddWarehouseProductAsync(WarehouseProduct warehouseProduct)
    {
        _context.WarehouseProducts.Add(warehouseProduct);
        return Task.CompletedTask;
    }

    public Task UpdateWarehouseProductAsync(WarehouseProduct warehouseProduct)
    {
        _context.WarehouseProducts.Update(warehouseProduct);
        return Task.CompletedTask;
    }

    public Task AddWarehouseMovementProductAsync(WarehouseMovementProduct movement)
    {
        _context.WarehouseMovementProducts.Add(movement);
        return Task.CompletedTask;
    }

    public async Task<List<WarehouseResource>> GetWarehouseResourcesByResourceIdAsync(Guid warehouseId, Guid resourceId)
    {
        return await _context.WarehouseResources
            .Where(wr => wr.WarehouseId == warehouseId && wr.ResourceId == resourceId && wr.Quantity > 0)
            .OrderBy(wr => wr.EntryDate)
            .ToListAsync();
    }

    public Task UpdateWarehouseResourceAsync(WarehouseResource warehouseResource)
    {
        _context.WarehouseResources.Update(warehouseResource);
        return Task.CompletedTask;
    }

    public Task AddWarehouseMovementResourceAsync(WarehouseMovementResource movement)
    {
        _context.WarehouseMovementResources.Add(movement);
        return Task.CompletedTask;
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
