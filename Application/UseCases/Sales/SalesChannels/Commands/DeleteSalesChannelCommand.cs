using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.SalesChannels.Commands;

public class DeleteSalesChannelCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSalesChannelCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var repo = _unitOfWork.GetRepository<SalesChannel>();

        var salesChannel = await repo.GetByIdAsync(id);
        if (salesChannel == null)
            throw new KeyNotFoundException($"Canal de ventas con ID {id} no encontrado");

        repo.Remove(salesChannel);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
