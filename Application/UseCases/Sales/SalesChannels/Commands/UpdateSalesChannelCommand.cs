using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.SalesChannels.Commands;

public class UpdateSalesChannelCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSalesChannelCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SalesChannelDto> ExecuteAsync(Guid id, UpdateSalesChannelDto dto)
    {
        var repo = _unitOfWork.GetRepository<SalesChannel>();

        var salesChannel = await repo.GetByIdAsync(id);
        if (salesChannel == null)
            throw new KeyNotFoundException($"Canal de ventas con ID {id} no encontrado");

        var exists = await repo.AnyAsync(sc => sc.Name == dto.Name && sc.Id != id);
        if (exists)
            throw new InvalidOperationException($"Ya existe otro canal de ventas con el nombre '{dto.Name}'");

        salesChannel.Name = dto.Name;
        salesChannel.UpdatedAt = DateTime.UtcNow;

        repo.Update(salesChannel);
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
