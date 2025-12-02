using Application.DTOs.Museum;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.TypePersons.Commands;

// Caso de uso para actualizar un tipo de persona existente
public class UpdateTypePersonUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTypePersonUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TypePersonDto> ExecuteAsync(Guid id, UpdateTypePersonDto dto)
    {
        var repository = _unitOfWork.GetRepository<TypePerson>();

        // Obtener el tipo de persona existente
        var typePerson = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Tipo de persona con ID {id} no encontrado");

        // Actualizar los campos proporcionados
        if (dto.Name is not null)
            typePerson.Name = dto.Name;

        if (dto.BasePrice.HasValue)
            typePerson.BasePrice = dto.BasePrice.Value;

        typePerson.UpdatedAt = DateTime.UtcNow;

        // Guardar los cambios
        repository.Update(typePerson);
        await _unitOfWork.SaveChangesAsync();

        // Retornar el DTO del tipo de persona actualizado
        return new TypePersonDto
        {
            Id = typePerson.Id,
            Name = typePerson.Name,
            BasePrice = typePerson.BasePrice,
            CreatedAt = typePerson.CreatedAt,
            UpdatedAt = typePerson.UpdatedAt
        };
    }
}
