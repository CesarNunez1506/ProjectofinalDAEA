using Application.DTOs.Finance;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.MonasteryExpenses;

/// <summary>
/// Caso de uso para obtener todos los gastos del monasterio
/// </summary>
public class GetAllMonasteryExpensesUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllMonasteryExpensesUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<MonasteryExpenseDto>> ExecuteAsync()
    {
        var repository = _unitOfWork.GetRepository<MonasteryExpense>();

        var expenses = await repository.GetAsync(
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
