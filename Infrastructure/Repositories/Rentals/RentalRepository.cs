using Domain.Entities;
using Domain.Interfaces.Repositories.Rentals;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Rentals;

public class RentalRepository : IRentalRepository
{
    private readonly AppDbContext _context;

    public RentalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Rental>> GetAllAsync()
    {
        return await _context.Rentals.ToListAsync();
    }

    public async Task<IEnumerable<Rental>> GetAllWithRelationsAsync()
    {
        return await _context.Rentals
            .Include(r => r.Customer)
            .Include(r => r.Place)
                .ThenInclude(p => p.Location)
            .Include(r => r.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<Rental>> GetActiveAsync()
    {
        return await _context.Rentals
            .Where(r => r.Status == true)
            .Include(r => r.Customer)
            .Include(r => r.Place)
            .Include(r => r.User)
            .ToListAsync();
    }

    public async Task<Rental?> GetByIdAsync(Guid id)
    {
        return await _context.Rentals.FindAsync(id);
    }

    public async Task<Rental?> GetByIdWithRelationsAsync(Guid id)
    {
        return await _context.Rentals
            .Include(r => r.Customer)
            .Include(r => r.Place)
                .ThenInclude(p => p.Location)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Rental>> GetByCustomerIdAsync(Guid customerId)
    {
        return await _context.Rentals
            .Where(r => r.CustomerId == customerId)
            .Include(r => r.Place)
            .Include(r => r.User)
            .OrderByDescending(r => r.StartDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Rental>> GetByPlaceIdAsync(Guid placeId)
    {
        return await _context.Rentals
            .Where(r => r.PlaceId == placeId)
            .Include(r => r.Customer)
            .Include(r => r.User)
            .OrderByDescending(r => r.StartDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Rental>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Rentals
            .Where(r => r.StartDate >= startDate && r.EndDate <= endDate)
            .Include(r => r.Customer)
            .Include(r => r.Place)
            .Include(r => r.User)
            .OrderBy(r => r.StartDate)
            .ToListAsync();
    }

    public async Task<Rental?> CheckOverlapAsync(Guid placeId, DateTime startDate, DateTime endDate, Guid? excludeRentalId = null)
    {
        var query = _context.Rentals
            .Where(r => r.PlaceId == placeId && r.Status == true)
            .Where(r => r.StartDate <= endDate && r.EndDate >= startDate);

        if (excludeRentalId.HasValue)
        {
            query = query.Where(r => r.Id != excludeRentalId.Value);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<Rental> CreateAsync(Rental rental)
    {
        rental.CreatedAt = DateTime.UtcNow;
        rental.UpdatedAt = DateTime.UtcNow;
        
        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();
        
        return rental;
    }

    public async Task<Rental> UpdateAsync(Rental rental)
    {
        rental.UpdatedAt = DateTime.UtcNow;
        
        _context.Rentals.Update(rental);
        await _context.SaveChangesAsync();
        
        return rental;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var rental = await GetByIdAsync(id);
        if (rental == null) return false;

        _context.Rentals.Remove(rental);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<Rental> ToggleStatusAsync(Guid id)
    {
        var rental = await GetByIdAsync(id);
        if (rental == null)
            throw new KeyNotFoundException($"Rental con ID {id} no encontrado");

        rental.Status = !rental.Status;
        rental.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        
        return rental;
    }
}
