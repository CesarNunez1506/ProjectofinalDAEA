using Application.DTOs.Museum;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.TypePersons.Queries;

// Caso de uso para obtener un tipo de persona por ID
public class GetTypePersonByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTypePersonByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TypePersonDto> ExecuteAsync(Guid id)
    {
        var repository = _unitOfWork.GetRepository<TypePerson>();

        // Obtener el tipo de persona por ID
        var typePerson = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Tipo de persona con ID {id} no encontrado");

        // Retornar el DTO del tipo de persona
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
