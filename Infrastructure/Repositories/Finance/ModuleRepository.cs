using Domain.Entities;
using Domain.Interfaces.Repositories.Finance;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Finance;

public class ModuleRepository : IModuleRepository
{
    private readonly AppDbContext _context;

    public ModuleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Module>> GetAllAsync()
    {
        return await _context.Modules.ToListAsync();
    }

    public async Task<Module?> GetByIdAsync(Guid id)
    {
        return await _context.Modules.FindAsync(id);
    }

    public async Task<Module?> GetByNameAsync(string name)
    {
        return await _context.Modules
            .FirstOrDefaultAsync(m => m.Name == name);
    }
}
