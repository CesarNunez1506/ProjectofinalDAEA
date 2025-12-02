using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.Stores.Commands;

/// <summary>
/// Caso de uso para eliminar una tienda
/// </summary>
public class DeleteStoreCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStoreCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var repo = _unitOfWork.GetRepository<Store>();

        var store = await repo.GetByIdAsync(id);
        if (store == null)
            throw new KeyNotFoundException($"Tienda con ID {id} no encontrada");

        // Validar que no haya ventas o sesiones de caja asociadas
        var salesRepo = _unitOfWork.GetRepository<Sale>();
        var hasSales = await salesRepo.AnyAsync(s => s.StoreId == id);
        if (hasSales)
            throw new InvalidOperationException("No se puede eliminar la tienda porque tiene ventas asociadas");

        var cashSessionRepo = _unitOfWork.GetRepository<CashSession>();
        var storeIdStr = id.ToString();
        var hasCashSessions = await cashSessionRepo.AnyAsync(cs => cs.StoreId == storeIdStr);
        if (hasCashSessions)
            throw new InvalidOperationException("No se puede eliminar la tienda porque tiene sesiones de caja asociadas");

        repo.Remove(store);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
