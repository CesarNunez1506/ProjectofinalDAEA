using Domain.Entities;
using Domain.Interfaces.Repositories.Finance;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Finance;

public class GeneralIncomeRepository : IGeneralIncomeRepository
{
    private readonly AppDbContext _context;

    public GeneralIncomeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GeneralIncome> CreateAsync(GeneralIncome income)
    {
        income.CreatedAt = DateTime.UtcNow;
        income.UpdatedAt = DateTime.UtcNow;
        
        _context.GeneralIncomes.Add(income);
        await _context.SaveChangesAsync();
        
        return income;
    }

    public async Task<IEnumerable<GeneralIncome>> GetAllAsync()
    {
        return await _context.GeneralIncomes
            .Include(i => i.Module)
            .ToListAsync();
    }

    public async Task<GeneralIncome?> GetByIdAsync(Guid id)
    {
        return await _context.GeneralIncomes
            .Include(i => i.Module)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}
