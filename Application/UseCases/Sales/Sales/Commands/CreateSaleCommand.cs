using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.Sales.Commands;

public class CreateSaleCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSaleCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SaleDto> ExecuteAsync(CreateSaleDto dto)
    {
        var repo = _unitOfWork.GetRepository<Sale>();

        // Validar que la tienda existe
        var storeRepo = _unitOfWork.GetRepository<Store>();
        var storeExists = await storeRepo.AnyAsync(s => s.Id == dto.StoreId);
        if (!storeExists)
            throw new KeyNotFoundException($"Tienda con ID {dto.StoreId} no encontrada");

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            IncomeDate = dto.IncomeDate,
            StoreId = dto.StoreId,
            TotalIncome = dto.TotalIncome,
            Observations = dto.Observations,
            CreatedAt = DateTime.UtcNow
        };

        await repo.AddAsync(sale);
        await _unitOfWork.SaveChangesAsync();

        // TODO: Aquí se debería registrar el ingreso en general_incomes
        // según la lógica del backend de Node.js

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
