using Domain.Interfaces.Repositories.Inventory;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Inventory;

/// <summary>
/// Repositorio específico para Product
/// Hereda del GenericRepository y agrega métodos específicos del dominio
/// </summary>
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(LocalDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Obtener productos por categoría
    /// </summary>
    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }

    /// <summary>
    /// Obtener productos con stock bajo
    /// </summary>
    public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int minQuantity = 10)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.WarehouseProducts)
            .Where(p => p.WarehouseProducts.Sum(wp => wp.Quantity) < minQuantity)
            .ToListAsync();
    }

    /// <summary>
    /// Obtener productos activos
    /// </summary>
    public async Task<IEnumerable<Product>> GetActiveProductsAsync()
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => p.Status == true)
            .ToListAsync();
    }

    /// <summary>
    /// Obtener productos producibles
    /// </summary>
    public async Task<IEnumerable<Product>> GetProducibleProductsAsync()
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => p.Producible == true)
            .ToListAsync();
    }

    /// <summary>
    /// Buscar productos por nombre (búsqueda parcial)
    /// </summary>
    public async Task<IEnumerable<Product>> SearchByNameAsync(string name)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => p.Name.Contains(name))
            .ToListAsync();
    }

    /// <summary>
    /// Obtener producto con su categoría y stock actual
    /// </summary>
    public async Task<Product?> GetProductWithDetailsAsync(Guid productId)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.WarehouseProducts)
                .ThenInclude(wp => wp.Warehouse)
            .FirstOrDefaultAsync(p => p.Id == productId);
    }
}
