using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.Entrances.Commands;

// Caso de uso para eliminar una entrada por su ID
public class DeleteEntranceUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEntranceUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    // Ejecuta el caso de uso para eliminar una entrada
    public async Task ExecuteAsync(Guid id)
    {
        var repository = _unitOfWork.GetRepository<Entrance>();

        var entrance = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Entrada con ID {id} no encontrada");

        repository.Remove(entrance);
        await _unitOfWork.SaveChangesAsync();
    }
}
