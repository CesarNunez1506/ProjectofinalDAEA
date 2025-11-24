using Domain.Entities;
using Domain.Interfaces.Repositories.Production;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Production;

/// <summary>
/// Implementación del repositorio de recetas usando EF Core
/// </summary>
public class RecipeRepository : IRecipeRepository
{
    private readonly LocalDbContext _context;

    public RecipeRepository(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Recipe>> GetAllWithRelationsAsync()
    {
        return await _context.Recipes
            .Include(r => r.Product)
            .Include(r => r.Resource)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Recipe?> GetByIdAsync(Guid id)
    {
        return await _context.Recipes
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Recipe>> GetByProductIdAsync(Guid productId)
    {
        return await _context.Recipes
            .Include(r => r.Resource)
            .AsNoTracking()
            .Where(r => r.ProductId == productId)
            .ToListAsync();
    }

    public async Task<bool> ExistsByProductAndResourceAsync(Guid productId, Guid resourceId)
    {
        return await _context.Recipes
            .AnyAsync(r => r.ProductId == productId && r.ResourceId == resourceId);
    }

    public async Task<Recipe> CreateAsync(Recipe recipe)
    {
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
        return recipe;
    }

    public async Task<Recipe> UpdateAsync(Recipe recipe)
    {
        _context.Recipes.Update(recipe);
        await _context.SaveChangesAsync();
        return recipe;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe == null)
            return false;

        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Recipes.AnyAsync(r => r.Id == id);
    }
}
