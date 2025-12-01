using Domain.Interfaces.Repositories;

namespace Domain.Interfaces.Services;

/// <summary>
/// Interfaz para manejar transacciones de base de datos
/// </summary>
public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task<int> SaveChangesAsync();
    
    /// <summary>
    /// Obtiene un repositorio genérico para la entidad solicitada
    /// </summary>
    IRepository<T> GetRepository<T>() where T : class;
}
