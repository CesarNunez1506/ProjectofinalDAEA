using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.Sales.Commands;

public class UpdateSaleCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSaleCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SaleDto> ExecuteAsync(Guid id, UpdateSaleDto dto)
    {
        var repo = _unitOfWork.GetRepository<Sale>();

        var sale = await repo.GetByIdAsync(id);
        if (sale == null)
            throw new KeyNotFoundException($"Venta con ID {id} no encontrada");

        // Validar que la tienda existe
        var storeRepo = _unitOfWork.GetRepository<Store>();
        var storeExists = await storeRepo.AnyAsync(s => s.Id == dto.StoreId);
        if (!storeExists)
            throw new KeyNotFoundException($"Tienda con ID {dto.StoreId} no encontrada");

        sale.IncomeDate = dto.IncomeDate;
        sale.StoreId = dto.StoreId;
        sale.TotalIncome = dto.TotalIncome;
        sale.Observations = dto.Observations;
        sale.UpdatedAt = DateTime.UtcNow;

        repo.Update(sale);
        await _unitOfWork.SaveChangesAsync();

        return new SaleDto
        {
            Id = sale.Id,
            IncomeDate = sale.IncomeDate,
            StoreId = sale.StoreId,
            TotalIncome = sale.TotalIncome,
            Observations = sale.Observations,
            CreatedAt = sale.CreatedAt,
            UpdatedAt = sale.UpdatedAt
        };
    }
}
