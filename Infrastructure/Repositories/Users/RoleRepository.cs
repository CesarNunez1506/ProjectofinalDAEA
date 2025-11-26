using Domain.Entities;
using Domain.Interfaces.Repositories.Users;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users;

public class RoleRepository : IRoleRepository
{
    private readonly LocalDbContext _context;

    public RoleRepository(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetByIdAsync(Guid id)
    {
        return await _context.Roles
            .Include(r => r.RolesPermissions)
                .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _context.Roles
            .Include(r => r.RolesPermissions)
                .ThenInclude(rp => rp.Permission)
            .OrderBy(r => r.Name)
            .ToListAsync();
    }

    public async Task<Role> CreateAsync(Role role)
    {
        role.CreatedAt = DateTime.UtcNow;
        role.UpdatedAt = DateTime.UtcNow;
        
        await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();
        
        return role;
    }

    public async Task<Role> UpdateAsync(Role role)
    {
        role.UpdatedAt = DateTime.UtcNow;
        
        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
        
        return role;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role != null)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Roles.AnyAsync(r => r.Id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Roles.AnyAsync(r => r.Name == name);
    }

    public async Task<int> CountAsync()
    {
        return await _context.Roles.CountAsync();
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _context.Roles
            .Include(r => r.RolesPermissions)
                .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(Guid roleId)
    {
        return await _context.RolesPermissions
            .Where(rp => rp.RoleId == roleId)
            .Include(rp => rp.Permission)
                .ThenInclude(p => p.Module)
            .Select(rp => rp.Permission)
            .OrderBy(p => p.Module!.Name)
            .ToListAsync();
    }

    public async Task AddPermissionsAsync(Guid roleId, IEnumerable<Guid> permissionIds)
    {
        var rolesPermissions = permissionIds.Select(permissionId => new RolesPermission
        {
            RoleId = roleId,
            PermissionId = permissionId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await _context.RolesPermissions.AddRangeAsync(rolesPermissions);
        await _context.SaveChangesAsync();
    }

    public async Task RemovePermissionsAsync(Guid roleId, IEnumerable<Guid> permissionIds)
    {
        var rolesPermissions = await _context.RolesPermissions
            .Where(rp => rp.RoleId == roleId && permissionIds.Contains(rp.PermissionId))
            .ToListAsync();

        _context.RolesPermissions.RemoveRange(rolesPermissions);
        await _context.SaveChangesAsync();
    }
}
