using Domain.Entities;
using Domain.Interfaces.Repositories.Users;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users;

public class PermissionRepository : IPermissionRepository
{
    private readonly LocalDbContext _context;

    public PermissionRepository(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<Permission?> GetByIdAsync(Guid id)
    {
        return await _context.Permissions
            .Include(p => p.Module)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Permission>> GetAllAsync()
    {
        return await _context.Permissions
            .Include(p => p.Module)
            .OrderBy(p => p.Module!.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Permission>> GetByModuleIdAsync(Guid moduleId)
    {
        return await _context.Permissions
            .Include(p => p.Module)
            .Where(p => p.ModuleId == moduleId)
            .ToListAsync();
    }

    public async Task<Permission> CreateAsync(Permission permission)
    {
        permission.CreatedAt = DateTime.UtcNow;
        permission.UpdatedAt = DateTime.UtcNow;
        
        await _context.Permissions.AddAsync(permission);
        await _context.SaveChangesAsync();
        
        return permission;
    }

    public async Task<Permission> UpdateAsync(Permission permission)
    {
        permission.UpdatedAt = DateTime.UtcNow;
        
        _context.Permissions.Update(permission);
        await _context.SaveChangesAsync();
        
        return permission;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var permission = await _context.Permissions.FindAsync(id);
        if (permission != null)
        {
            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Permissions.AnyAsync(p => p.Id == id);
    }

    public async Task<bool> ExistsForModuleAsync(Guid moduleId)
    {
        return await _context.Permissions.AnyAsync(p => p.ModuleId == moduleId);
    }

    public async Task<int> CountAsync()
    {
        return await _context.Permissions.CountAsync();
    }
}
