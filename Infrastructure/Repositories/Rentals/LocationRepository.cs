using Domain.Entities;
using Domain.Interfaces.Repositories.Rentals;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Rentals;

public class LocationRepository : ILocationRepository
{
    private readonly AppDbContext _context;

    public LocationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        return await _context.Locations.ToListAsync();
    }

    public async Task<Location?> GetByIdAsync(Guid id)
    {
        return await _context.Locations.FindAsync(id);
    }

    public async Task<IEnumerable<Location>> GetByStatusAsync(string status)
    {
        return await _context.Locations
            .Where(l => l.Status == status)
            .ToListAsync();
    }

    public async Task<Location> CreateAsync(Location location)
    {
        location.CreatedAt = DateTime.UtcNow;
        location.UpdatedAt = DateTime.UtcNow;
        
        _context.Locations.Add(location);
        await _context.SaveChangesAsync();
        
        return location;
    }

    public async Task<Location> UpdateAsync(Location location)
    {
        location.UpdatedAt = DateTime.UtcNow;
        
        _context.Locations.Update(location);
        await _context.SaveChangesAsync();
        
        return location;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var location = await GetByIdAsync(id);
        if (location == null) return false;

        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();
        
        return true;
    }
}
