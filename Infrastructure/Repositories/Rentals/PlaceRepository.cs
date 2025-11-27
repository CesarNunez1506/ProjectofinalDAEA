using Domain.Entities;
using Domain.Interfaces.Repositories.Rentals;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Rentals;

public class PlaceRepository : IPlaceRepository
{
    private readonly AppDbContext _context;

    public PlaceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Place>> GetAllAsync()
    {
        return await _context.Places.ToListAsync();
    }

    public async Task<IEnumerable<Place>> GetAllWithRelationsAsync()
    {
        return await _context.Places
            .Include(p => p.Location)
            .ToListAsync();
    }

    public async Task<Place?> GetByIdAsync(Guid id)
    {
        return await _context.Places.FindAsync(id);
    }

    public async Task<Place?> GetByIdWithRelationsAsync(Guid id)
    {
        return await _context.Places
            .Include(p => p.Location)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Place>> GetByLocationIdAsync(Guid locationId)
    {
        return await _context.Places
            .Where(p => p.LocationId == locationId)
            .ToListAsync();
    }

    public async Task<Place> CreateAsync(Place place)
    {
        place.CreatedAt = DateTime.UtcNow;
        place.UpdatedAt = DateTime.UtcNow;
        
        _context.Places.Add(place);
        await _context.SaveChangesAsync();
        
        return place;
    }

    public async Task<Place> UpdateAsync(Place place)
    {
        place.UpdatedAt = DateTime.UtcNow;
        
        _context.Places.Update(place);
        await _context.SaveChangesAsync();
        
        return place;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var place = await GetByIdAsync(id);
        if (place == null) return false;

        _context.Places.Remove(place);
        await _context.SaveChangesAsync();
        
        return true;
    }
}
