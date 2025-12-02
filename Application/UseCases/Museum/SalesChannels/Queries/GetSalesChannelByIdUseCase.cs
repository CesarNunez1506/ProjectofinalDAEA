using Application.DTOs.Museum;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.SalesChannels.Queries;

// Caso de uso para obtener un canal de venta por ID
public class GetSalesChannelByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSalesChannelByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SalesChannelDto> ExecuteAsync(Guid id)
    {
        var repository = _unitOfWork.GetRepository<SalesChannel>();

        // Obtener el canal de venta por ID
        var salesChannel = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Canal de venta con ID {id} no encontrado");

        // Retornar el DTO del canal de venta
        return new SalesChannelDto
        {
            Id = salesChannel.Id,
            Name = salesChannel.Name,
            CreatedAt = salesChannel.CreatedAt,
            UpdatedAt = salesChannel.UpdatedAt
        };
    }
}
