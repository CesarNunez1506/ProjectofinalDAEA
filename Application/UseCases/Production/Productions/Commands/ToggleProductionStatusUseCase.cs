using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Productions;

/// <summary>
/// Caso de uso para activar/desactivar una producción
/// </summary>
public class ToggleProductionStatusUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public ToggleProductionStatusUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var productionRepo = _unitOfWork.GetRepository<Domain.Entities.Production>();
        
        var production = await productionRepo.GetByIdAsync(id);
        if (production == null)
        {
            throw new KeyNotFoundException($"No se encontró la producción con ID {id}");
        }

        production.IsActive = !production.IsActive;
        production.UpdatedAt = DateTime.UtcNow;
        productionRepo.Update(production);
        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }
}
