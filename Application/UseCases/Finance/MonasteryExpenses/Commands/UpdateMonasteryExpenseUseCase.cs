using Application.UseCases.Finance.MonasteryExpenses.Commands;
using Application.DTOs.Finance;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.MonasteryExpenses.Commands
{

/// <summary>
/// Caso de uso para actualizar un gasto del monasterio
/// </summary>
public class UpdateMonasteryExpenseUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMonasteryExpenseUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MonasteryExpenseDto> ExecuteAsync(Guid id, UpdateMonasteryExpenseDto dto)
    {
        var repository = _unitOfWork.GetRepository<MonasteryExpense>();

        var expense = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"No se encontró el gasto con ID {id}");

        // Actualizar solo los campos proporcionados
        if (dto.Category is not null)
            expense.Category = dto.Category;
        if (dto.Amount.HasValue)
            expense.Amount = dto.Amount.Value;
        if (dto.Name is not null)
            expense.Name = dto.Name;
        if (dto.Date.HasValue)
            expense.Date = DateTime.SpecifyKind(dto.Date.Value, DateTimeKind.Utc);
        if (dto.Descripcion is not null)
            expense.Descripcion = dto.Descripcion;

        expense.UpdatedAt = DateTime.UtcNow;

        repository.Update(expense);
        await _unitOfWork.SaveChangesAsync();

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
}
