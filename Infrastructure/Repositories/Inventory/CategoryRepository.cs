using Domain.Interfaces.Repositories.Inventory;
using Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Inventory;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(LocalDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Category>> GetActiveCategoriesAsync()
    {
        return await _dbSet
            .Where(c => c.Status == true)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<Category?> GetCategoryWithProductsAsync(Guid categoryId)
    {
        return await _dbSet
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == categoryId);
    }

    public async Task<IEnumerable<Category>> SearchByNameAsync(string name)
    {
        return await _dbSet
            .Where(c => c.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<bool> HasProductsAsync(Guid categoryId)
    {
        return await _context.Products
            .AnyAsync(p => p.CategoryId == categoryId);
    }
}
