using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Losts;

/// <summary>
/// Caso de uso para eliminar (hard delete) una pérdida
/// </summary>
public class DeleteLostUseCase
{
    private readonly ILostRepository _lostRepository;

    public DeleteLostUseCase(ILostRepository lostRepository)
    {
        _lostRepository = lostRepository;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var lost = await _lostRepository.GetByIdAsync(id);
        if (lost == null)
        {
            throw new KeyNotFoundException($"No se encontró la pérdida con ID {id}");
        }

        // Hard delete - elimina permanentemente
        return await _lostRepository.DeleteAsync(id);
    }
}
