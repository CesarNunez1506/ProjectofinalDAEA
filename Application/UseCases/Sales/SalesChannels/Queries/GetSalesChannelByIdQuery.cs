using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.SalesChannels.Queries;

public class GetSalesChannelByIdQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSalesChannelByIdQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SalesChannelDto?> ExecuteAsync(Guid id)
    {
        var repo = _unitOfWork.GetRepository<SalesChannel>();
        var channel = await repo.GetByIdAsync(id);

        if (channel == null)
            return null;

        return new SalesChannelDto
        {
            Id = channel.Id,
            Name = channel.Name,
            CreatedAt = channel.CreatedAt,
            UpdatedAt = channel.UpdatedAt
        };
    }
}
