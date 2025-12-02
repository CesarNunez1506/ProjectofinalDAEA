using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Repositories.Finance;

namespace Infrastructure.Repositories
{
    // Repositorios con lógica específica que no es solo CRUD genérico
    public class FinancialReportSpecificRepository : GenericRepository<FinancialReport>, IFinancialReportGenericRepository
    {
        private readonly AppDbContext _context;

        public FinancialReportSpecificRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FinancialReport?> GetReportByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Include(r => r.GeneralIncomes)
                .Include(r => r.GeneralExpenses)
                .FirstOrDefaultAsync(r => r.StartDate >= startDate && r.EndDate <= endDate);
        }

        public async Task<bool> ExistsInPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await AnyAsync(r => r.StartDate == startDate && r.EndDate == endDate);
        }

        public async Task<IEnumerable<GeneralIncome>> GetIncomesAsync(Guid reportId)
        {
            return await _context.GeneralIncomes
                .Where(g => g.ReportId == reportId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<GeneralExpense>> GetExpensesAsync(Guid reportId)
        {
            return await _context.GeneralExpenses
                .Where(e => e.ReportId == reportId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateTotalsAsync(Guid reportId, decimal income, decimal expense, decimal netProfit)
        {
            var report = await GetByIdAsync(reportId);

            if (report == null) return;

            report.TotalIncome = income;
            report.TotalExpenses = expense;
            report.NetProfit = netProfit;
            report.UpdatedAt = DateTime.UtcNow;

            Update(report);
            await _context.SaveChangesAsync();
        }

        public void DeleteReport(FinancialReport entity)
        {
            Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }

    public class OverheadSpecificRepository : GenericRepository<Overhead>, IOverheadGenericRepository
    {
        private readonly AppDbContext _context;

        public OverheadSpecificRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Overhead>> GetByTypeAsync(string type)
        {
            return await _dbSet
                .Where(o => o.Type == type)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Overhead>> GetByStatusAsync(bool status)
        {
            return await _dbSet
                .Where(o => o.Status == status)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Overhead>> GetByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(o => o.Date >= startDate && o.Date <= endDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(o => o.Date >= startDate && o.Date <= endDate)
                .SumAsync(o => o.Amount);
        }

        public async Task<Overhead?> GetWithExpensesAsync(Guid overheadId)
        {
            return await _dbSet
                .Include(o => o.MonasteryExpenses)
                .FirstOrDefaultAsync(o => o.Id == overheadId);
        }

        public void DeleteOverhead(Overhead entity)
        {
            Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }

    // Adaptadores que delegan en el repositorio genérico pero viven en el módulo Finanzas
    public class GeneralIncomeRepositoryAdapter : GenericRepository<GeneralIncome>, IGeneralIncomeGenericRepository
    {
        public GeneralIncomeRepositoryAdapter(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<GeneralIncome>> GetIncomesByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(i => i.Date >= startDate && i.Date <= endDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<GeneralIncome>> GetByReportIdAsync(Guid reportId)
        {
            return await _dbSet
                .Where(i => i.ReportId == reportId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<GeneralIncome>> GetByModuleAsync(Guid moduleId)
        {
            return await _dbSet
                .Where(i => i.ModuleId == moduleId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(i => i.Date >= startDate && i.Date <= endDate)
                .SumAsync(i => i.Amount);
        }

        public void DeleteIncome(GeneralIncome entity)
        {
            Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }

    public class GeneralExpenseRepositoryAdapter : GenericRepository<GeneralExpense>, IGeneralExpenseGenericRepository
    {
        public GeneralExpenseRepositoryAdapter(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<GeneralExpense>> GetExpensesByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<GeneralExpense>> GetByReportIdAsync(Guid reportId)
        {
            return await _dbSet
                .Where(e => e.ReportId == reportId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<GeneralExpense>> GetByModuleAsync(Guid moduleId)
        {
            return await _dbSet
                .Where(e => e.ModuleId == moduleId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .SumAsync(e => e.Amount);
        }

        public void DeleteExpense(GeneralExpense entity)
        {
            Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }

    public class MonasteryExpenseRepositoryAdapter : GenericRepository<MonasteryExpense>, IMonasteryExpenseGenericRepository
    {
        public MonasteryExpenseRepositoryAdapter(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MonasteryExpense>> GetExpensesByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<MonasteryExpense>> GetByCategoryAsync(string category)
        {
            return await _dbSet
                .Where(e => e.Category == category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<MonasteryExpense>> GetByOverheadIdAsync(Guid overheadId)
        {
            return await _dbSet
                .Where(e => e.OverheadsId == overheadId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<double> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .SumAsync(e => e.Amount);
        }

        public void DeleteExpense(MonasteryExpense entity)
        {
            Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
