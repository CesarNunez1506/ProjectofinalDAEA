using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interfaces.Repositories.Finance;

namespace Infrastructure.Repositories;

/// <summary>
/// Implementación del repositorio genérico usando EF Core
/// </summary>
public class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).AsNoTracking().ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "",
        int? skip = null,
        int? take = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
            query = query.Where(filter);

        // Incluir propiedades de navegación
        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty.Trim());
        }

        if (orderBy != null)
            query = orderBy(query);

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (take.HasValue)
            query = query.Take(take.Value);

        return await query.AsNoTracking().ToListAsync();
    }

    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void UpdateRange(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public virtual void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
    {
        if (predicate == null)
            return await _dbSet.CountAsync();
        return await _dbSet.CountAsync(predicate);
    }
}

// aqui repositiro finace mo modulo finance
// Implementaciones específicas reutilizando el repositorio genérico

public class GeneralIncomeGenericRepository : GenericRepository<GeneralIncome>, IGeneralIncomeGenericRepository
{
    public GeneralIncomeGenericRepository(AppDbContext context) : base(context)
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

public class GeneralExpenseGenericRepository : GenericRepository<GeneralExpense>, IGeneralExpenseGenericRepository
{
    public GeneralExpenseGenericRepository(AppDbContext context) : base(context)
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

public class MonasteryExpenseGenericRepository : GenericRepository<MonasteryExpense>, IMonasteryExpenseGenericRepository
{
    public MonasteryExpenseGenericRepository(AppDbContext context) : base(context)
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