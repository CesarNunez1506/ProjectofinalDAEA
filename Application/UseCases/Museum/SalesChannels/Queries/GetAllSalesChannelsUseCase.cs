using Application.DTOs.Museum;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.SalesChannels.Queries;

// Caso de uso para obtener todos los canales de venta
public class GetAllSalesChannelsUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllSalesChannelsUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SalesChannelDto>> ExecuteAsync()
    {
        var repository = _unitOfWork.GetRepository<SalesChannel>();

        // Obtener todos los canales de venta ordenados por nombre
        var salesChannels = await repository.GetAsync(
            orderBy: q => q.OrderBy(sc => sc.Name)
        );

        // Mapear las entidades a DTOs
        return salesChannels.Select(sc => new SalesChannelDto
        {
            Id = sc.Id,
            Name = sc.Name,
            CreatedAt = sc.CreatedAt,
            UpdatedAt = sc.UpdatedAt
        });
    }
}
