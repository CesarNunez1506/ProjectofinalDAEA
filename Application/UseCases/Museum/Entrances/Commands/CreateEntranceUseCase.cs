using Application.DTOs.Museum;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Museum.Entrances.Commands;

// Caso de uso para crear una nueva entrada
public class CreateEntranceUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateEntranceUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<EntranceDto> ExecuteAsync(CreateEntranceDto dto)
    {
        var repository = _unitOfWork.GetRepository<Entrance>();

        // Validar existencia de las entidades relacionadas
        var userRepo = _unitOfWork.GetRepository<User>();
        var typePersonRepo = _unitOfWork.GetRepository<TypePerson>();
        var salesChannelRepo = _unitOfWork.GetRepository<SalesChannel>();
        var paymentMethodRepo = _unitOfWork.GetRepository<PaymentMethod>();

        var user = await userRepo.GetByIdAsync(dto.UserId)
            ?? throw new KeyNotFoundException($"Usuario con ID {dto.UserId} no encontrado");

        var typePerson = await typePersonRepo.GetByIdAsync(dto.TypePersonId)
            ?? throw new KeyNotFoundException($"Tipo de persona con ID {dto.TypePersonId} no encontrado");

        var salesChannel = await salesChannelRepo.GetByIdAsync(dto.SaleChannel)
            ?? throw new KeyNotFoundException($"Canal de venta con ID {dto.SaleChannel} no encontrado");

        var paymentMethod = await paymentMethodRepo.GetByIdAsync(dto.PaymentMethod)
            ?? throw new KeyNotFoundException($"Método de pago con ID {dto.PaymentMethod} no encontrado");
        
        // Crear la nueva entrada
        var entrance = new Entrance
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            TypePersonId = dto.TypePersonId,
            SaleDate = dto.SaleDate,
            Cantidad = dto.Cantidad,
            SaleNumber = dto.SaleNumber,
            SaleChannel = dto.SaleChannel,
            TotalSale = dto.TotalSale,
            PaymentMethod = dto.PaymentMethod,
            Free = dto.Free,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Agregar la nueva entrada al repositorio
        await repository.AddAsync(entrance);
        await _unitOfWork.SaveChangesAsync();

        // Retornar el DTO de la entrada creada
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
            UserName = user.Name,
            TypePersonName = typePerson.Name,
            SaleChannelName = salesChannel.Name,
            PaymentMethodName = paymentMethod.Name
        };
    }
}
