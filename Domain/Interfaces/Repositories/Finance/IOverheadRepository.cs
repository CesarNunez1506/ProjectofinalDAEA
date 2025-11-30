using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.Finance
{
    /// <summary>
    /// Repositorio específico para Overhead
    /// Hereda de IRepository para operaciones genéricas
    /// </summary>
    public interface IOverheadRepository : IRepository<Overhead>
    {
        // Métodos específicos del repositorio
        Task<IEnumerable<Overhead>> GetByTypeAsync(string type);
        Task<IEnumerable<Overhead>> GetByStatusAsync(bool status);
        Task<IEnumerable<Overhead>> GetByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<Overhead?> GetWithExpensesAsync(Guid overheadId);
    }
}
