using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.Sales.Commands;

public class DeleteSaleCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSaleCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var repo = _unitOfWork.GetRepository<Sale>();

        var sale = await repo.GetByIdAsync(id);
        if (sale == null)
            throw new KeyNotFoundException($"Venta con ID {id} no encontrada");

        // Validar que no haya detalles de venta asociados
        var saleDetailRepo = _unitOfWork.GetRepository<SaleDetail>();
        var hasDetails = await saleDetailRepo.AnyAsync(sd => sd.SaleId == id);
        if (hasDetails)
            throw new InvalidOperationException("No se puede eliminar la venta porque tiene detalles asociados");

        repo.Remove(sale);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
