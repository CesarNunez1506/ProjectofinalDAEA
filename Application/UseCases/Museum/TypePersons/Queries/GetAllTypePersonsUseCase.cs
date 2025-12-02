using Application.DTOs.Museum;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.TypePersons.Queries;

// Caso de uso para obtener todos los tipos de persona
public class GetAllTypePersonsUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTypePersonsUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TypePersonDto>> ExecuteAsync()
    {
        var repository = _unitOfWork.GetRepository<TypePerson>();

        // Obtener todos los tipos de persona ordenados por nombre
        var typePersons = await repository.GetAsync(
            orderBy: q => q.OrderBy(tp => tp.Name)
        );

        // Mapear las entidades a DTOs
        return typePersons.Select(tp => new TypePersonDto
        {
            Id = tp.Id,
            Name = tp.Name,
            BasePrice = tp.BasePrice,
            CreatedAt = tp.CreatedAt,
            UpdatedAt = tp.UpdatedAt
        });
    }
}
