using Application.DTOs.Finance;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.MonasteryExpenses;

/// <summary>
/// Caso de uso para crear un nuevo gasto del monasterio
/// </summary>
public class CreateMonasteryExpenseUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMonasteryExpenseUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MonasteryExpenseDto> ExecuteAsync(CreateMonasteryExpenseDto dto)
    {
        var repository = _unitOfWork.GetRepository<MonasteryExpense>();

        var expense = new MonasteryExpense
        {
            Id = Guid.NewGuid(),
            Category = dto.Category,
            Amount = dto.Amount,
            Name = dto.Name,
            Date = DateTime.SpecifyKind(dto.Date, DateTimeKind.Utc),
            Descripcion = dto.Descripcion ?? string.Empty,
            OverheadsId = null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(expense);
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
