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
}
