using Domain.Interfaces.Repositories.Sales;

namespace Application.UseCases.CashSessions;

/// <summary>
/// Cierra una sesión de caja.
/// </summary>
public class CloseCashSessionUseCase
{
    private readonly ICashSessionRepository _cashSessionRepository;

    public CloseCashSessionUseCase(ICashSessionRepository cashSessionRepository)
    {
        _cashSessionRepository = cashSessionRepository;
    }

    public async Task ExecuteAsync(Guid sessionId, decimal closingAmount)
    {
        var session = await _cashSessionRepository.GetByIdAsync(sessionId);

        if (session == null)
            throw new InvalidOperationException("La sesión no existe.");

        if (session.Status == "CLOSED")
            throw new InvalidOperationException("La sesión ya está cerrada.");

        session.ClosingAmount = closingAmount;
        session.Status = "CLOSED";
        session.UpdatedAt = DateTime.UtcNow;

        await _cashSessionRepository.UpdateAsync(session);
    }
}
