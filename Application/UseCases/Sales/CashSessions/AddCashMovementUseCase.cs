using Application.DTOs.CashSessions;
using Domain.Entities;
using Domain.Interfaces.Repositories.Sales;

namespace Application.UseCases.CashSessions;

/// <summary>
/// Agrega un movimiento (ingreso o egreso) a la sesión de caja.
/// </summary>
public class AddCashMovementUseCase
{
    private readonly ICashSessionRepository _cashSessionRepository;

    public AddCashMovementUseCase(ICashSessionRepository cashSessionRepository)
    {
        _cashSessionRepository = cashSessionRepository;
    }

    public async Task ExecuteAsync(Guid sessionId, AddCashMovementDto dto)
    {
        var session = await _cashSessionRepository.GetByIdAsync(sessionId);

        if (session == null)
            throw new InvalidOperationException("La sesión no existe.");

        if (session.Status != "OPEN")
            throw new InvalidOperationException("Solo se permiten movimientos en sesiones abiertas.");

        var movement = new CashMovement
        {
            Id = Guid.NewGuid(),
            CashSessionId = sessionId,
            Type = dto.Type,           // "IN" o "OUT"
            Amount = dto.Amount,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow,
        };

        await _cashSessionRepository.AddMovementAsync(movement);
    }
}
