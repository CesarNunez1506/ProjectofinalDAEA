using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.CashSessions.Commands;

public class UpdateCashSessionCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCashSessionCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CashSessionDto> ExecuteAsync(Guid id, UpdateCashSessionDto dto)
    {
        var repo = _unitOfWork.GetRepository<CashSession>();

        var cashSession = await repo.GetByIdAsync(id);
        if (cashSession == null)
            throw new KeyNotFoundException($"Sesión de caja con ID {id} no encontrada");

        // Actualizar campos opcionales
        if (dto.EndAmount.HasValue)
            cashSession.EndAmount = dto.EndAmount.Value;

        if (dto.TotalSales.HasValue)
            cashSession.TotalSales = dto.TotalSales.Value;

        if (dto.TotalReturns.HasValue)
            cashSession.TotalReturns = dto.TotalReturns.Value;

        if (dto.EndedAt.HasValue)
            cashSession.EndedAt = dto.EndedAt.Value;

        cashSession.UpdatedAt = DateTime.UtcNow;

        repo.Update(cashSession);
        await _unitOfWork.SaveChangesAsync();

        return new CashSessionDto
        {
            Id = cashSession.Id,
            UserId = cashSession.UserId,
            StoreId = cashSession.StoreId,
            StartAmount = cashSession.StartAmount,
            EndAmount = cashSession.EndAmount,
            TotalSales = cashSession.TotalSales,
            TotalReturns = cashSession.TotalReturns,
            StartedAt = cashSession.StartedAt,
            EndedAt = cashSession.EndedAt,
            CreatedAt = cashSession.CreatedAt,
            UpdatedAt = cashSession.UpdatedAt
        };
    }
}
