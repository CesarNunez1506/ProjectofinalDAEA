using Application.DTOs.Museum;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.Entrances.Queries;

// Caso de uso para obtener una entrada por su ID
public class GetEntranceByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetEntranceByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // Ejecuta el caso de uso para obtener una entrada por su ID
    public async Task<EntranceDto> ExecuteAsync(Guid id)
    {
        var repository = _unitOfWork.GetRepository<Entrance>();

        var entrances = await repository.GetAsync(
            filter: e => e.Id == id,
            includeProperties: "User,TypePerson,SaleChannelNavigation,PaymentMethodNavigation"
        );

        var entrance = entrances.FirstOrDefault()
            ?? throw new KeyNotFoundException($"Entrada con ID {id} no encontrada");

        // Retornar el DTO de la entrada encontrada
        return new EntranceDto
        {
            Id = entrance.Id,
            UserId = entrance.UserId,
            TypePersonId = entrance.TypePersonId,
            SaleDate = entrance.SaleDate,
            Cantidad = entrance.Cantidad,
            SaleNumber = entrance.SaleNumber,
            SaleChannel = entrance.SaleChannel,
            TotalSale = entrance.TotalSale,
            PaymentMethod = entrance.PaymentMethod,
            Free = entrance.Free,
            CreatedAt = entrance.CreatedAt,
            UpdatedAt = entrance.UpdatedAt,
            UserName = entrance.User?.Name,
            TypePersonName = entrance.TypePerson?.Name,
            SaleChannelName = entrance.SaleChannelNavigation?.Name,
            PaymentMethodName = entrance.PaymentMethodNavigation?.Name
        };
    }
}
