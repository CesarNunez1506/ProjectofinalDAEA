using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.Returns.Commands;

public class UpdateReturnCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateReturnCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ReturnDto> ExecuteAsync(Guid id, UpdateReturnDto dto)
    {
        var repo = _unitOfWork.GetRepository<Return>();

        var returnEntity = await repo.GetByIdAsync(id);
        if (returnEntity == null)
            throw new KeyNotFoundException($"Devolución con ID {id} no encontrada");

        // Actualizar campos opcionales
        if (dto.ProductId.HasValue) returnEntity.ProductId = dto.ProductId;
        if (dto.SalesId.HasValue) returnEntity.SalesId = dto.SalesId;
        if (dto.StoreId.HasValue) returnEntity.StoreId = dto.StoreId;
        if (dto.Reason != null) returnEntity.Reason = dto.Reason;
        if (dto.Observations != null) returnEntity.Observations = dto.Observations;
        if (dto.Quantity.HasValue) returnEntity.Quantity = dto.Quantity.Value;
        if (dto.Price.HasValue) returnEntity.Price = dto.Price.Value;
        
        returnEntity.UpdatedAt = DateTime.UtcNow;

        repo.Update(returnEntity);
        await _unitOfWork.SaveChangesAsync();

        return new ReturnDto
        {
            Id = returnEntity.Id,
            ProductId = returnEntity.ProductId,
            SalesId = returnEntity.SalesId,
            StoreId = returnEntity.StoreId,
            Reason = returnEntity.Reason,
            Observations = returnEntity.Observations,
            Quantity = returnEntity.Quantity,
            Price = returnEntity.Price,
            CreatedAt = returnEntity.CreatedAt,
            UpdatedAt = returnEntity.UpdatedAt
        };
    }
}
