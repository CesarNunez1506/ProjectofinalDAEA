using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Inventory;
using Domain.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Inventory;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Services;

/// <summary>
/// Implementación del patrón Unit of Work para manejar transacciones y repositorios
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;
    private readonly Dictionary<Type, object> _repositories = new();

    // Repositorios lazy-loaded
    private IProductRepository? _products;
    private ICategoryRepository? _categories;
    private Domain.Interfaces.Repositories.Inventory.IWarehouseRepository? _warehouses;
    private ISupplierRepository? _suppliers;
    private IResourceRepository? _resources;
    private IWarehouseProductRepository? _warehouseProducts;
    private IWarehouseResourceRepository? _warehouseResources;
    private IBuysProductRepository? _buysProducts;
    private IWarehouseMovementProductRepository? _warehouseMovementProducts;
    private IWarehouseMovementResourceRepository? _warehouseMovementResources;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    // ============================================
    // PROPIEDADES DE REPOSITORIOS
    // ============================================

    public IProductRepository Products => _products ??= new ProductRepository(_context);
    public ICategoryRepository Categories => _categories ??= new CategoryRepository(_context);
    public Domain.Interfaces.Repositories.Inventory.IWarehouseRepository Warehouses => _warehouses ??= new Infrastructure.Repositories.Inventory.WarehouseRepository(_context);
    public ISupplierRepository Suppliers => _suppliers ??= new SupplierRepository(_context);
    public IResourceRepository Resources => _resources ??= new ResourceRepository(_context);
    public IWarehouseProductRepository WarehouseProducts => _warehouseProducts ??= new WarehouseProductRepository(_context);
    public IWarehouseResourceRepository WarehouseResources => _warehouseResources ??= new WarehouseResourceRepository(_context);
    public IBuysProductRepository BuysProducts => _buysProducts ??= new BuysProductRepository(_context);
    public IWarehouseMovementProductRepository WarehouseMovementProducts => _warehouseMovementProducts ??= new WarehouseMovementProductRepository(_context);
    public IWarehouseMovementResourceRepository WarehouseMovementResources => _warehouseMovementResources ??= new WarehouseMovementResourceRepository(_context);

    // ============================================
    // GESTIÓN DE TRANSACCIONES
    // ============================================

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public IRepository<T> GetRepository<T>() where T : class
    {
        var type = typeof(T);

        if (!_repositories.ContainsKey(type))
        {
            var repositoryInstance = new GenericRepository<T>(_context);
            _repositories[type] = repositoryInstance;
        }

        return (IRepository<T>)_repositories[type];
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context?.Dispose();
    }
}
