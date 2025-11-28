using Application.DTOs.Finance;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Overheads;

/// <summary>
/// Caso de uso para obtener un overhead por ID
/// </summary>
public class GetOverheadByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOverheadByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OverheadDto?> ExecuteAsync(Guid id)
    {
        var overheadRepo = _unitOfWork.GetRepository<Overhead>();
        var expenseRepo = _unitOfWork.GetRepository<MonasteryExpense>();

        var overhead = await overheadRepo.GetByIdAsync(id);

        if (overhead == null)
            return null;

        var expenses = await expenseRepo.FindAsync(e => e.OverheadsId == id);

        return new OverheadDto
        {
            Id = overhead.Id,
            Name = overhead.Name,
            Date = overhead.Date,
            Type = overhead.Type,
            Amount = overhead.Amount,
            Description = overhead.Description,
            Status = overhead.Status,
            CreatedAt = overhead.CreatedAt,
            UpdatedAt = overhead.UpdatedAt,
            MonasteryExpenses = expenses.Select(e => new MonasteryExpenseDto
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
            }).ToList()
        };
    }
}
