using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Domain.Interfaces.Repositories
{
    public interface IOverheadRepository
    {
        // CRUD básico
        Task<IEnumerable<Overhead>> GetAllAsync();
        Task<Overhead?> GetByIdAsync(Guid id);
        Task AddAsync(Overhead entity);
        void Update(Overhead entity);
        void Delete(Overhead entity);
        Task<bool> SaveChangesAsync();

        // Obtener por tipo de overhead (ej: mantenimiento, servicios, etc.)
        Task<IEnumerable<Overhead>> GetByTypeAsync(string type);

        // Obtener activos/inactivos
        Task<IEnumerable<Overhead>> GetByStatusAsync(bool status);

        // Obtener por rango de fechas
        Task<IEnumerable<Overhead>> GetByPeriodAsync(DateTime startDate, DateTime endDate);

        // Total de overheads dentro de un periodo
        Task<decimal> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate);

        // Obtener Overhead con sus gastos relacionados
        Task<Overhead?> GetWithExpensesAsync(Guid overheadId);
    }
}
