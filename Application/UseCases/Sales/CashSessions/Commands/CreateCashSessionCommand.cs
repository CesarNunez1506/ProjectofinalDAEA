using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Sales.CashSessions.Commands;

public class CreateCashSessionCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCashSessionCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CashSessionDto> ExecuteAsync(CreateCashSessionDto dto, string userId)
    {
        var repo = _unitOfWork.GetRepository<CashSession>();

        // Verificar que no haya una sesión abierta para esta tienda (EndedAt == null)
        var existingOpenSession = await repo.FirstOrDefaultAsync(cs => 
            cs.StoreId == dto.StoreId && cs.EndedAt == null);

        if (existingOpenSession != null)
            throw new InvalidOperationException("Ya existe una sesión de caja abierta para esta tienda");

        // Validar que la tienda existe
        var storeRepo = _unitOfWork.GetRepository<Store>();
        var storeId = Guid.Parse(dto.StoreId);
        var storeExists = await storeRepo.AnyAsync(s => s.Id == storeId);
        if (!storeExists)
            throw new KeyNotFoundException($"Tienda con ID {dto.StoreId} no encontrada");

        var cashSession = new CashSession
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            StoreId = dto.StoreId,
            StartAmount = dto.StartAmount,
            StartedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await repo.AddAsync(cashSession);
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
