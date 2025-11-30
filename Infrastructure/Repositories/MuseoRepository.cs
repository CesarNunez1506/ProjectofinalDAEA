using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MuseoRepository : IMuseoRepository
{
    private readonly AppDbContext _context;

    public MuseoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Museo?> GetByIdAsync(Guid id)
    {
        return await _context.Museos
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Museo>> GetAllAsync()
    {
        return await _context.Museos
            .OrderBy(m => m.Nombre)
            .ToListAsync();
    }

    public async Task<Museo> AddAsync(Museo museo)
    {
        _context.Museos.Add(museo);
        await _context.SaveChangesAsync();
        return museo;
    }

    public async Task UpdateAsync(Museo museo)
    {
        _context.Museos.Update(museo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Museo museo)
    {
        _context.Museos.Remove(museo);
        await _context.SaveChangesAsync();
    }
}
