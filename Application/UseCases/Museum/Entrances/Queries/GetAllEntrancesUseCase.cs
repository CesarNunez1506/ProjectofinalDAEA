using Application.DTOs.Museum;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.Entrances.Queries;

// Caso de uso para obtener todas las entradas
public class GetAllEntrancesUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllEntrancesUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // Ejecuta el caso de uso para obtener todas las entradas
    public async Task<IEnumerable<EntranceDto>> ExecuteAsync()
    {
        var repository = _unitOfWork.GetRepository<Entrance>();

        var entrances = await repository.GetAsync(
            orderBy: q => q.OrderByDescending(e => e.CreatedAt),
            includeProperties: "User,TypePerson,SaleChannelNavigation,PaymentMethodNavigation"
        );

        // Retornar la lista de DTOs de entradas
        return entrances.Select(e => new EntranceDto
        {
            Id = e.Id,
            UserId = e.UserId,
            TypePersonId = e.TypePersonId,
            SaleDate = e.SaleDate,
            Cantidad = e.Cantidad,
            SaleNumber = e.SaleNumber,
            SaleChannel = e.SaleChannel,
            TotalSale = e.TotalSale,
            PaymentMethod = e.PaymentMethod,
            Free = e.Free,
            CreatedAt = e.CreatedAt,
            UpdatedAt = e.UpdatedAt,
            UserName = e.User?.Name,
            TypePersonName = e.TypePerson?.Name,
            SaleChannelName = e.SaleChannelNavigation?.Name,
            PaymentMethodName = e.PaymentMethodNavigation?.Name
        });
    }
}
