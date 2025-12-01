using Domain.Interfaces.Repositories.Sales;

namespace Application.UseCases.CashSessions;

/// <summary>
/// Realiza la conciliación de caja.
/// </summary>
public class ReconcileCashUseCase
{
    private readonly ICashSessionRepository _cashSessionRepository;

    public ReconcileCashUseCase(ICashSessionRepository cashSessionRepository)
    {
        _cashSessionRepository = cashSessionRepository;
    }

    public async Task<decimal> ExecuteAsync(Guid sessionId)
    {
        var session = await _cashSessionRepository.GetByIdAsync(sessionId);

        if (session == null)
            throw new InvalidOperationException("La sesión no existe.");

        if (session.Status != "CLOSED")
            throw new InvalidOperationException("Solo se puede conciliar una sesión cerrada.");

        var movements = await _cashSessionRepository.GetMovementsBySessionIdAsync(sessionId);

        var totalIn = movements.Where(m => m.Type == "IN").Sum(m => m.Amount);
        var totalOut = movements.Where(m => m.Type == "OUT").Sum(m => m.Amount);

        var expected = session.OpeningAmount + totalIn - totalOut;
        var difference = session.ClosingAmount - expected;

        return difference; // puede ser positivo, negativo o cero
    }
}
