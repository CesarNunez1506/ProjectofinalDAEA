using Application.DTOs.Finance;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Overheads.Queries;

/// <summary>
/// Caso de uso para obtener gastos del monasterio por rango de fechas
/// </summary>
public class GetExpensesByDateRangeUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetExpensesByDateRangeUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<MonasteryExpenseDto>> ExecuteAsync(DateTime startDate, DateTime endDate)
    {
        var repository = _unitOfWork.GetRepository<MonasteryExpense>();

        // Normalizar fechas a UTC
        var start = DateTime.SpecifyKind(startDate.Date, DateTimeKind.Utc);
        var end = DateTime.SpecifyKind(endDate.Date.AddDays(1).AddTicks(-1), DateTimeKind.Utc);

        var expenses = await repository.GetAsync(
            filter: e => e.Date >= start && e.Date <= end,
            orderBy: q => q.OrderByDescending(e => e.Date)
        );

        return expenses.Select(e => new MonasteryExpenseDto
        {
            Id = e.Id,
            Category = e.Category,
            Amount = e.Amount,
            Name = e.Name,
            Date = e.Date,
            Descripcion = e.Descripcion,
            OverheadsId = e.OverheadsId,
            CreatedAt = e.CreatedAt,
            UpdatedAt = e.UpdatedAt
        });
    }
}
