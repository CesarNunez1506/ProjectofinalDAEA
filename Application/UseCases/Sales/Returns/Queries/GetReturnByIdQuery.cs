using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.Returns.Queries;

public class GetReturnByIdQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetReturnByIdQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ReturnDto?> ExecuteAsync(Guid id)
    {
        var repo = _unitOfWork.GetRepository<Return>();
        var returnEntity = await repo.GetByIdAsync(id);

        if (returnEntity == null)
            return null;

        return new ReturnDto
        {
            Id = returnEntity.Id,
            ProductId = returnEntity.ProductId,
            SalesId = returnEntity.SalesId,
            StoreId = returnEntity.StoreId,
            Reason = returnEntity.Reason,
            Observations = returnEntity.Observations,
            Quantity = returnEntity.Quantity,
            Price = returnEntity.Price,
            CreatedAt = returnEntity.CreatedAt,
            UpdatedAt = returnEntity.UpdatedAt
        };
    }
}
