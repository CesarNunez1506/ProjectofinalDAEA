using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Losts;

/// <summary>
/// Caso de uso para eliminar (hard delete) una pérdida
/// </summary>
public class DeleteLostUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLostUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var lostRepo = _unitOfWork.GetRepository<Lost>();
        
        var lost = await lostRepo.GetByIdAsync(id);
        if (lost == null)
        {
            throw new KeyNotFoundException($"No se encontró la pérdida con ID {id}");
        }

        // Hard delete - elimina permanentemente
        lostRepo.Remove(lost);
        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }
}
