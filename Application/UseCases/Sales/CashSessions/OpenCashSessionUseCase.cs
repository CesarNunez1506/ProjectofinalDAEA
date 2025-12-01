using Application.DTOs.CashSessions;
using Domain.Entities;
using Domain.Interfaces.Repositories.Sales;

namespace Application.UseCases.CashSessions;

/// <summary>
/// Abre una nueva sesión de caja.
/// </summary>
public class OpenCashSessionUseCase
{
    private readonly ICashSessionRepository _cashSessionRepository;

    public OpenCashSessionUseCase(ICashSessionRepository cashSessionRepository)
    {
        _cashSessionRepository = cashSessionRepository;
    }

    public async Task<CashSessionDto> ExecuteAsync(OpenCashSessionDto dto)
    {
        // Validar si existe sesión abierta en la tienda
        var activeSession = await _cashSessionRepository.GetActiveSessionByStoreAsync(dto.StoreId);
        if (activeSession != null)
            throw new InvalidOperationException("Ya existe una sesión de caja abierta en esta tienda.");

        var session = new CashSession
        {
            Id = Guid.NewGuid(),
            StoreId = dto.StoreId,
            OpeningAmount = dto.OpeningAmount,
            Status = "OPEN",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _cashSessionRepository.CreateAsync(session);

        return new CashSessionDto
        {
            Id = created.Id,
            StoreId = created.StoreId,
            OpeningAmount = created.OpeningAmount,
            ClosingAmount = created.ClosingAmount,
            Status = created.Status,
            CreatedAt = created.CreatedAt
        };
    }
}
