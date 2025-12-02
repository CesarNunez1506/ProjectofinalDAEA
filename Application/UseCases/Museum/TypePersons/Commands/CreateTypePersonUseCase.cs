using Application.DTOs.Museum;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.TypePersons.Commands;

// Caso de uso para crear un nuevo tipo de persona
public class CreateTypePersonUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTypePersonUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TypePersonDto> ExecuteAsync(CreateTypePersonDto dto)
    {
        var repository = _unitOfWork.GetRepository<TypePerson>();

        // Crear el nuevo tipo de persona
        var typePerson = new TypePerson
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            BasePrice = dto.BasePrice,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Agregar el nuevo tipo de persona al repositorio
        await repository.AddAsync(typePerson);
        await _unitOfWork.SaveChangesAsync();

        // Retornar el DTO del tipo de persona creado
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
