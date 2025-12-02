using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.TypePersons.Commands;

// Caso de uso para eliminar un tipo de persona
public class DeleteTypePersonUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTypePersonUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid id)
    {
        var repository = _unitOfWork.GetRepository<TypePerson>();

        // Obtener el tipo de persona existente
        var typePerson = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Tipo de persona con ID {id} no encontrado");

        // Eliminar el tipo de persona
        repository.Remove(typePerson);
        await _unitOfWork.SaveChangesAsync();
    }
}
