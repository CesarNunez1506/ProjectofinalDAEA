using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.SalesChannels.Queries;

public class GetAllSalesChannelsQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllSalesChannelsQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SalesChannelDto>> ExecuteAsync()
    {
        var repo = _unitOfWork.GetRepository<SalesChannel>();
        var channels = await repo.GetAllAsync();

        return channels.Select(sc => new SalesChannelDto
        {
            Id = sc.Id,
            Name = sc.Name,
            CreatedAt = sc.CreatedAt,
            UpdatedAt = sc.UpdatedAt
        });
    }
}
