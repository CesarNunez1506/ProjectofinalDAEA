using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.Returns.Commands;

public class DeleteReturnCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReturnCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var repo = _unitOfWork.GetRepository<Return>();

        var returnEntity = await repo.GetByIdAsync(id);
        if (returnEntity == null)
            throw new KeyNotFoundException($"Devolución con ID {id} no encontrada");

        repo.Remove(returnEntity);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
