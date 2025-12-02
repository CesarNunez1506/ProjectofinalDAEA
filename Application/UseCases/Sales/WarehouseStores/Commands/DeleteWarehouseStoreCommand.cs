using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.WarehouseStores.Commands;

public class DeleteWarehouseStoreCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteWarehouseStoreCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var repo = _unitOfWork.GetRepository<WarehouseStore>();

        var warehouseStore = await repo.GetByIdAsync(id);
        if (warehouseStore == null)
            throw new KeyNotFoundException($"Registro de inventario con ID {id} no encontrado");

        repo.Remove(warehouseStore);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
