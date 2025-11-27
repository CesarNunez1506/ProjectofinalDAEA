using Domain.Entities;

namespace Domain.Interfaces.Repositories.Finance;

// Interface para el repositorio de reportes financieros
public interface IFinancialReportRepository
{
    // Obtiene el reporte financiero activo
    Task<FinancialReport?> GetActiveReportAsync();
    // Obtiene un reporte financiero por su ID
    Task<FinancialReport?> GetByIdAsync(Guid id);
}
