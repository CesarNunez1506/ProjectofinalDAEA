using Domain.Entities;
using Domain.Interfaces.Repositories.Users;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users;

public class ModuleRepository : IModuleRepository
{
    private readonly AppDbContext _context;

    public ModuleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Module?> GetByIdAsync(Guid id)
    {
        return await _context.Modules
            .Include(m => m.Permissions)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Module?> GetByNameAsync(string name)
    {
        return await _context.Modules
            .Include(m => m.Permissions)
            .FirstOrDefaultAsync(m => m.Name == name);
    }

    public async Task<IEnumerable<Module>> GetAllAsync()
    {
        return await _context.Modules
            .Include(m => m.Permissions)
            .OrderBy(m => m.Name)
            .ToListAsync();
    }

    public async Task<Module> CreateAsync(Module module)
    {
        module.CreatedAt = DateTime.UtcNow;
        module.UpdatedAt = DateTime.UtcNow;
        
        await _context.Modules.AddAsync(module);
        await _context.SaveChangesAsync();
        
        return module;
    }

    public async Task<Module> UpdateAsync(Module module)
    {
        module.UpdatedAt = DateTime.UtcNow;
        
        _context.Modules.Update(module);
        await _context.SaveChangesAsync();
        
        return module;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var module = await _context.Modules.FindAsync(id);
        if (module != null)
        {
            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Modules.AnyAsync(m => m.Id == id);
    }

    public async Task<int> CountAsync()
    {
        return await _context.Modules.CountAsync();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Modules.AnyAsync(m => m.Name == name);
    }
}
