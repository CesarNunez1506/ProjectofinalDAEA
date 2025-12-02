using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.SalesChannels.Commands;

// Caso de uso para eliminar un canal de venta
public class DeleteSalesChannelUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSalesChannelUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid id)
    {
        var repository = _unitOfWork.GetRepository<SalesChannel>();

        // Obtener el canal de venta existente
        var salesChannel = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Canal de venta con ID {id} no encontrado");

        // Eliminar el canal de venta
        repository.Remove(salesChannel);
        await _unitOfWork.SaveChangesAsync();
    }
}
