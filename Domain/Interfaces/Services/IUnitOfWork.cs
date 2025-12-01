using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Inventory;

namespace Domain.Interfaces.Services;

/// <summary>
/// Interfaz para manejar transacciones de base de datos y acceso a repositorios
/// Patrón Unit of Work - Agrupa todos los repositorios y maneja transacciones
/// </summary>
public interface IUnitOfWork : IDisposable
{
    // ============================================
    // REPOSITORIOS DE INVENTARIO
    // ============================================
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    Domain.Interfaces.Repositories.Inventory.IWarehouseRepository Warehouses { get; }
    ISupplierRepository Suppliers { get; }
    IResourceRepository Resources { get; }
    IWarehouseProductRepository WarehouseProducts { get; }
    IWarehouseResourceRepository WarehouseResources { get; }
    IBuysProductRepository BuysProducts { get; }
    IWarehouseMovementProductRepository WarehouseMovementProducts { get; }
    IWarehouseMovementResourceRepository WarehouseMovementResources { get; }

    // ============================================
    // GESTIÓN DE TRANSACCIONES
    // ============================================

    /// <summary>
    /// Inicia una transacción de base de datos
    /// </summary>
    Task BeginTransactionAsync();

    /// <summary>
    /// Confirma la transacción actual
    /// </summary>
    Task CommitTransactionAsync();

    /// <summary>
    /// Revierte la transacción actual
    /// </summary>
    Task RollbackTransactionAsync();

    /// <summary>
    /// Guarda todos los cambios pendientes en la base de datos
    /// </summary>
    Task<int> SaveChangesAsync();
    
    /// <summary>
    /// Obtiene un repositorio genérico para la entidad solicitada
    /// </summary>
    IRepository<T> GetRepository<T>() where T : class;
}
