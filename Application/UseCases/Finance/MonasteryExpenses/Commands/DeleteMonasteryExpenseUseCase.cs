using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.MonasteryExpenses;

/// <summary>
/// Caso de uso para eliminar un gasto del monasterio
/// </summary>
public class DeleteMonasteryExpenseUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMonasteryExpenseUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid id)
    {
        var repository = _unitOfWork.GetRepository<MonasteryExpense>();

        var expense = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"No se encontró el gasto con ID {id}");

        repository.Remove(expense);
        await _unitOfWork.SaveChangesAsync();
    }
}
