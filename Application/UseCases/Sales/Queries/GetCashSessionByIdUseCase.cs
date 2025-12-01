using Application.DTOs.CashSessions;
using Domain.Interfaces.Repositories.Sales;

namespace Application.UseCases.Sales.Queries;

public class GetCashSessionByIdUseCase
{
    private readonly ICashSessionRepository _cashSessionRepository;

    public GetCashSessionByIdUseCase(ICashSessionRepository cashSessionRepository)
    {
        _cashSessionRepository = cashSessionRepository;
    }

    public async Task<CashSessionDto?> ExecuteAsync(Guid sessionId)
    {
        var session = await _cashSessionRepository.GetByIdAsync(sessionId);

        if (session == null)
            return null;

        return new CashSessionDto
        {
            Id = session.Id,
            StoreId = session.StoreId,
            OpeningAmount = session.OpeningAmount,
            ClosingAmount = session.ClosingAmount,
            Status = session.Status,
            CreatedAt = session.CreatedAt
        };
    }
}
