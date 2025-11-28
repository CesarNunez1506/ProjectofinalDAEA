using Application.DTOs.Finance;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.MonasteryExpenses;

/// <summary>
/// Caso de uso para obtener un gasto del monasterio por ID
/// </summary>
public class GetMonasteryExpenseByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMonasteryExpenseByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MonasteryExpenseDto?> ExecuteAsync(Guid id)
    {
        var repository = _unitOfWork.GetRepository<MonasteryExpense>();

        var expense = await repository.GetByIdAsync(id);

        if (expense == null)
            return null;

        return new MonasteryExpenseDto
        {
            Id = expense.Id,
            Category = expense.Category,
            Amount = expense.Amount,
            Name = expense.Name,
            Date = expense.Date,
            Descripcion = expense.Descripcion,
            OverheadsId = expense.OverheadsId,
            CreatedAt = expense.CreatedAt,
            UpdatedAt = expense.UpdatedAt
        };
    }
}
