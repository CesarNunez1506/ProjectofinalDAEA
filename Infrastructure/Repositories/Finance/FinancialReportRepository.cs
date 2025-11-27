using Domain.Entities;
using Domain.Interfaces.Repositories.Finance;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Finance;

public class FinancialReportRepository : IFinancialReportRepository
{
    private readonly AppDbContext _context;

    public FinancialReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FinancialReport?> GetActiveReportAsync()
    {
        return await _context.FinancialReports
            .Where(r => r.EndDate == null)
            .OrderByDescending(r => r.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<FinancialReport?> GetByIdAsync(Guid id)
    {
        return await _context.FinancialReports.FindAsync(id);
    }
}
