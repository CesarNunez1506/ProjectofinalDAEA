using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.CashSessions.Commands;

public class DeleteCashSessionCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCashSessionCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var repo = _unitOfWork.GetRepository<CashSession>();

        var cashSession = await repo.GetByIdAsync(id);
        if (cashSession == null)
            throw new KeyNotFoundException($"Sesión de caja con ID {id} no encontrada");

        repo.Remove(cashSession);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
