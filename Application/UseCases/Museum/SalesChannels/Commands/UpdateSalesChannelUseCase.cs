using Application.DTOs.Museum;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.SalesChannels.Commands;

// Caso de uso para actualizar un canal de venta existente
public class UpdateSalesChannelUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSalesChannelUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SalesChannelDto> ExecuteAsync(Guid id, UpdateSalesChannelDto dto)
    {
        var repository = _unitOfWork.GetRepository<SalesChannel>();

        // Obtener el canal de venta existente
        var salesChannel = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Canal de venta con ID {id} no encontrado");

        // Actualizar los campos proporcionados
        if (dto.Name is not null)
            salesChannel.Name = dto.Name;

        salesChannel.UpdatedAt = DateTime.UtcNow;

        // Guardar los cambios
        repository.Update(salesChannel);
        await _unitOfWork.SaveChangesAsync();

        // Retornar el DTO del canal de venta actualizado
        return new SalesChannelDto
        {
            Id = salesChannel.Id,
            Name = salesChannel.Name,
            CreatedAt = salesChannel.CreatedAt,
            UpdatedAt = salesChannel.UpdatedAt
        };
    }
}
