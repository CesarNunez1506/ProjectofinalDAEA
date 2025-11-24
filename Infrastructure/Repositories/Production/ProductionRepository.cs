using Domain.Entities;
using Domain.Interfaces.Repositories.Production;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ProductionEntity = Domain.Entities.Production;

namespace Infrastructure.Repositories.Production;

/// <summary>
/// Implementación del repositorio de producción usando EF Core
/// </summary>
public class ProductionRepository : IProductionRepository
{
    private readonly LocalDbContext _context;

    public ProductionRepository(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductionEntity>> GetAllAsync()
    {
        return await _context.Productions
            .AsNoTracking()
            .OrderByDescending(p => p.ProductionDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductionEntity>> GetAllWithRelationsAsync()
    {
        return await _context.Productions
            .Include(p => p.Product)
            .Include(p => p.Plant)
            .AsNoTracking()
            .OrderByDescending(p => p.ProductionDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductionEntity>> GetActiveAsync()
    {
        return await _context.Productions
            .Include(p => p.Product)
            .Include(p => p.Plant)
            .AsNoTracking()
            .Where(p => p.IsActive == true)
            .OrderByDescending(p => p.ProductionDate)
            .ToListAsync();
    }

    public async Task<ProductionEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Productions
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<ProductionEntity?> GetByIdWithRelationsAsync(Guid id)
    {
        return await _context.Productions
            .Include(p => p.Product)
                .ThenInclude(pr => pr.Category)
            .Include(p => p.Plant)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<ProductionEntity>> GetByProductIdAsync(Guid productId)
    {
        return await _context.Productions
            .Include(p => p.Plant)
            .AsNoTracking()
            .Where(p => p.ProductId == productId)
            .OrderByDescending(p => p.ProductionDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductionEntity>> GetByPlantIdAsync(Guid plantId)
    {
        return await _context.Productions
            .Include(p => p.Product)
            .AsNoTracking()
            .Where(p => p.PlantId == plantId)
            .OrderByDescending(p => p.ProductionDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductionEntity>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Productions
            .Include(p => p.Product)
            .Include(p => p.Plant)
            .AsNoTracking()
            .Where(p => p.ProductionDate >= startDate && p.ProductionDate <= endDate)
            .OrderByDescending(p => p.ProductionDate)
            .ToListAsync();
    }

    public async Task<ProductionEntity> CreateAsync(ProductionEntity production)
    {
        _context.Productions.Add(production);
        await _context.SaveChangesAsync();
        return production;
    }

    public async Task<ProductionEntity> UpdateAsync(ProductionEntity production)
    {
        _context.Productions.Update(production);
        await _context.SaveChangesAsync();
        return production;
    }

    public async Task<bool> ToggleActiveStatusAsync(Guid id)
    {
        var production = await _context.Productions.FindAsync(id);
        if (production == null)
            return false;

        production.IsActive = !production.IsActive;
        production.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Productions.AnyAsync(p => p.Id == id);
    }
}
