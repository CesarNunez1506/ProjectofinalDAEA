using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.SalesChannels.Commands;

public class CreateSalesChannelCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSalesChannelCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SalesChannelDto> ExecuteAsync(CreateSalesChannelDto dto)
    {
        var repo = _unitOfWork.GetRepository<SalesChannel>();

        var exists = await repo.AnyAsync(sc => sc.Name == dto.Name);
        if (exists)
            throw new InvalidOperationException($"Ya existe un canal de ventas con el nombre '{dto.Name}'");

        var salesChannel = new SalesChannel
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            CreatedAt = DateTime.UtcNow
        };

        await repo.AddAsync(salesChannel);
        await _unitOfWork.SaveChangesAsync();

        return new SalesChannelDto
        {
            Id = salesChannel.Id,
            Name = salesChannel.Name,
            CreatedAt = salesChannel.CreatedAt,
            UpdatedAt = salesChannel.UpdatedAt
        };
    }
}
