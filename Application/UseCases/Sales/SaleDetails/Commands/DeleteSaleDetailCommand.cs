using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.SaleDetails.Commands;

public class DeleteSaleDetailCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSaleDetailCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var repo = _unitOfWork.GetRepository<SaleDetail>();

        var saleDetail = await repo.GetByIdAsync(id);
        if (saleDetail == null)
            throw new KeyNotFoundException($"Detalle de venta con ID {id} no encontrado");

        repo.Remove(saleDetail);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
