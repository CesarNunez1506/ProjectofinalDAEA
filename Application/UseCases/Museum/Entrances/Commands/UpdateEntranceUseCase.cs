using Application.DTOs.Museum;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.Entrances.Commands;

// Caso de uso para actualizar una entrada existente
public class UpdateEntranceUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEntranceUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<EntranceDto> ExecuteAsync(Guid id, UpdateEntranceDto dto)
    {
        var repository = _unitOfWork.GetRepository<Entrance>();

        var entrance = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Entrada con ID {id} no encontrada");

        // Obtener repositorios de entidades relacionadas
        var userRepo = _unitOfWork.GetRepository<User>();
        var typePersonRepo = _unitOfWork.GetRepository<TypePerson>();
        var salesChannelRepo = _unitOfWork.GetRepository<SalesChannel>();
        var paymentMethodRepo = _unitOfWork.GetRepository<PaymentMethod>();

        // Validar existencia de las entidades relacionadas si se proporcionan y actualizar
        User? user = null;
        if (dto.UserId.HasValue)
        {
            user = await userRepo.GetByIdAsync(dto.UserId.Value)
                ?? throw new KeyNotFoundException($"Usuario con ID {dto.UserId} no encontrado");
            entrance.UserId = dto.UserId.Value;
        }

        TypePerson? typePerson = null;
        if (dto.TypePersonId.HasValue)
        {
            typePerson = await typePersonRepo.GetByIdAsync(dto.TypePersonId.Value)
                ?? throw new KeyNotFoundException($"Tipo de persona con ID {dto.TypePersonId} no encontrado");
            entrance.TypePersonId = dto.TypePersonId.Value;
        }

        SalesChannel? salesChannel = null;
        if (dto.SaleChannel.HasValue)
        {
            salesChannel = await salesChannelRepo.GetByIdAsync(dto.SaleChannel.Value)
                ?? throw new KeyNotFoundException($"Canal de venta con ID {dto.SaleChannel} no encontrado");
            entrance.SaleChannel = dto.SaleChannel.Value;
        }

        PaymentMethod? paymentMethod = null;
        if (dto.PaymentMethod.HasValue)
        {
            paymentMethod = await paymentMethodRepo.GetByIdAsync(dto.PaymentMethod.Value)
                ?? throw new KeyNotFoundException($"Método de pago con ID {dto.PaymentMethod} no encontrado");
            entrance.PaymentMethod = dto.PaymentMethod.Value;
        }

        // Actualizar campos simples
        if (dto.SaleDate is not null)
            entrance.SaleDate = dto.SaleDate;

        if (dto.Cantidad.HasValue)
            entrance.Cantidad = dto.Cantidad.Value;

        if (dto.SaleNumber is not null)
            entrance.SaleNumber = dto.SaleNumber;

        if (dto.TotalSale.HasValue)
            entrance.TotalSale = dto.TotalSale.Value;

        if (dto.Free.HasValue)
            entrance.Free = dto.Free.Value;

        entrance.UpdatedAt = DateTime.UtcNow;

        repository.Update(entrance);
        await _unitOfWork.SaveChangesAsync();

        // Cargar entidades relacionadas para el DTO de respuesta (solo si no fueron cargadas antes)
        if (user == null)
            user = await userRepo.GetByIdAsync(entrance.UserId);
        if (typePerson == null)
            typePerson = await typePersonRepo.GetByIdAsync(entrance.TypePersonId);
        if (salesChannel == null)
            salesChannel = await salesChannelRepo.GetByIdAsync(entrance.SaleChannel);
        if (paymentMethod == null)
            paymentMethod = await paymentMethodRepo.GetByIdAsync(entrance.PaymentMethod);

        // Retornar el DTO actualizado
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
            UserName = user?.Name,
            TypePersonName = typePerson?.Name,
            SaleChannelName = salesChannel?.Name,
            PaymentMethodName = paymentMethod?.Name
        };
    }
}
