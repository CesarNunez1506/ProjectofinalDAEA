using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.CashSessions.Queries;

public class GetAllCashSessionsQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCashSessionsQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CashSessionDto>> ExecuteAsync()
    {
        var repo = _unitOfWork.GetRepository<CashSession>();
        var sessions = await repo.GetAllAsync();

        return sessions.Select(cs => new CashSessionDto
        {
            Id = cs.Id,
            UserId = cs.UserId,
            StoreId = cs.StoreId,
            StartAmount = cs.StartAmount,
            EndAmount = cs.EndAmount,
            TotalSales = cs.TotalSales,
            TotalReturns = cs.TotalReturns,
            StartedAt = cs.StartedAt,
            EndedAt = cs.EndedAt,
            CreatedAt = cs.CreatedAt,
            UpdatedAt = cs.UpdatedAt
        });
    }
}
