using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.CashSessions.Queries;

public class GetCashSessionByIdQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCashSessionByIdQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CashSessionDto?> ExecuteAsync(Guid id)
    {
        var repo = _unitOfWork.GetRepository<CashSession>();
        var session = await repo.GetByIdAsync(id);

        if (session == null)
            return null;

        return new CashSessionDto
        {
            Id = session.Id,
            UserId = session.UserId,
            StoreId = session.StoreId,
            StartAmount = session.StartAmount,
            EndAmount = session.EndAmount,
            TotalSales = session.TotalSales,
            TotalReturns = session.TotalReturns,
            StartedAt = session.StartedAt,
            EndedAt = session.EndedAt,
            CreatedAt = session.CreatedAt,
            UpdatedAt = session.UpdatedAt
        };
    }
}
