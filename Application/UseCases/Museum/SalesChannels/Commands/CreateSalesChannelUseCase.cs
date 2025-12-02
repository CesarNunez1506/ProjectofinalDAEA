using Application.DTOs.Museum;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.SalesChannels.Commands;

// Caso de uso para crear un nuevo canal de venta
public class CreateSalesChannelUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSalesChannelUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SalesChannelDto> ExecuteAsync(CreateSalesChannelDto dto)
    {
        var repository = _unitOfWork.GetRepository<SalesChannel>();

        // Crear el nuevo canal de venta
        var salesChannel = new SalesChannel
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Agregar el nuevo canal de venta al repositorio
        await repository.AddAsync(salesChannel);
        await _unitOfWork.SaveChangesAsync();

        // Retornar el DTO del canal de venta creado
        return new SalesChannelDto
        {
            Id = salesChannel.Id,
            Name = salesChannel.Name,
            CreatedAt = salesChannel.CreatedAt,
            UpdatedAt = salesChannel.UpdatedAt
        };
    }
}
