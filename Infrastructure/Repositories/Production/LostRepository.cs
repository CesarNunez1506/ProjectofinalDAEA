using Domain.Entities;
using Domain.Interfaces.Repositories.Production;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Production;

/// <summary>
/// Implementación del repositorio de pérdidas usando EF Core
/// </summary>
public class LostRepository : ILostRepository
{
    private readonly LocalDbContext _context;

    public LostRepository(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Lost>> GetAllWithProductionAsync()
    {
        return await _context.Losts
            .Include(l => l.Production)
                .ThenInclude(p => p.Product)
            .AsNoTracking()
            .OrderByDescending(l => l.CreatedAt)
            .ToListAsync();
    }

    public async Task<Lost?> GetByIdAsync(Guid id)
    {
        return await _context.Losts
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<IEnumerable<Lost>> GetByProductionIdAsync(Guid productionId)
    {
        return await _context.Losts
            .AsNoTracking()
            .Where(l => l.ProductionId == productionId)
            .OrderByDescending(l => l.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Lost>> GetByLostTypeAsync(string lostType)
    {
        return await _context.Losts
            .Include(l => l.Production)
            .AsNoTracking()
            .Where(l => l.LostType.ToLower() == lostType.ToLower())
            .OrderByDescending(l => l.CreatedAt)
            .ToListAsync();
    }

    public async Task<Lost> CreateAsync(Lost lost)
    {
        _context.Losts.Add(lost);
        await _context.SaveChangesAsync();
        return lost;
    }

    public async Task<Lost> UpdateAsync(Lost lost)
    {
        _context.Losts.Update(lost);
        await _context.SaveChangesAsync();
        return lost;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var lost = await _context.Losts.FindAsync(id);
        if (lost == null)
            return false;

        _context.Losts.Remove(lost);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Losts.AnyAsync(l => l.Id == id);
    }
}
