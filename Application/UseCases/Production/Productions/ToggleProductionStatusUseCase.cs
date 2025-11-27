using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Productions;

/// <summary>
/// Caso de uso para activar/desactivar una producción
/// </summary>
public class ToggleProductionStatusUseCase
{
    private readonly IProductionRepository _productionRepository;

    public ToggleProductionStatusUseCase(IProductionRepository productionRepository)
    {
        _productionRepository = productionRepository;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var production = await _productionRepository.GetByIdAsync(id);
        if (production == null)
        {
            throw new KeyNotFoundException($"No se encontró la producción con ID {id}");
        }

        return await _productionRepository.ToggleActiveStatusAsync(id);
    }
}
