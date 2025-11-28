using Domain.Entities;
using Domain.Interfaces.Repositories.Production;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Production;

/// <summary>
/// Implementación del repositorio de plantas de producción usando EF Core
/// </summary>
public class PlantProductionRepository : IPlantProductionRepository
{
    private readonly AppDbContext _context;

    public PlantProductionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PlantProduction>> GetAllAsync()
    {
        return await _context.PlantProductions
            .AsNoTracking()
            .OrderBy(p => p.PlantName)
            .ToListAsync();
    }

    public async Task<IEnumerable<PlantProduction>> GetActiveAsync()
    {
        return await _context.PlantProductions
            .AsNoTracking()
            .Where(p => p.Status == true)
            .OrderBy(p => p.PlantName)
            .ToListAsync();
    }

    public async Task<PlantProduction?> GetByIdAsync(Guid id)
    {
        return await _context.PlantProductions
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PlantProduction?> GetByIdWithWarehouseAsync(Guid id)
    {
        return await _context.PlantProductions
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PlantProduction> CreateAsync(PlantProduction plant)
    {
        _context.PlantProductions.Add(plant);
        await _context.SaveChangesAsync();
        return plant;
    }

    public async Task<PlantProduction> UpdateAsync(PlantProduction plant)
    {
        _context.PlantProductions.Update(plant);
        await _context.SaveChangesAsync();
        return plant;
    }

    public async Task<bool> SoftDeleteAsync(Guid id)
    {
        var plant = await _context.PlantProductions.FindAsync(id);
        if (plant == null)
            return false;

        plant.Status = false;
        plant.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.PlantProductions.AnyAsync(p => p.Id == id);
    }
}
