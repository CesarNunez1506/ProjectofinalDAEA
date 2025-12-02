using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.Returns.Queries;

public class GetAllReturnsQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllReturnsQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ReturnDto>> ExecuteAsync()
    {
        var repo = _unitOfWork.GetRepository<Return>();
        var returns = await repo.GetAllAsync();

        return returns.Select(r => new ReturnDto
        {
            Id = r.Id,
            ProductId = r.ProductId,
            SalesId = r.SalesId,
            StoreId = r.StoreId,
            Reason = r.Reason,
            Observations = r.Observations,
            Quantity = r.Quantity,
            Price = r.Price,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt
        });
    }
}
