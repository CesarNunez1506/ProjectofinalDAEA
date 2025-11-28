using Application.DTOs.Finance;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Overheads;

/// <summary>
/// Caso de uso para actualizar un overhead
/// </summary>
public class UpdateOverheadUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOverheadUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OverheadDto> ExecuteAsync(Guid id, UpdateOverheadDto dto)
    {
        var repository = _unitOfWork.GetRepository<Overhead>();

        var overhead = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"No se encontró el overhead con ID {id}");

        // Actualizar solo los campos proporcionados
        if (dto.Name is not null)
            overhead.Name = dto.Name;
        if (dto.Date.HasValue)
            overhead.Date = DateTime.SpecifyKind(dto.Date.Value, DateTimeKind.Utc);
        if (dto.Amount.HasValue)
            overhead.Amount = dto.Amount.Value;
        if (dto.Description is not null)
            overhead.Description = dto.Description;
        if (dto.Status.HasValue)
            overhead.Status = dto.Status.Value;

        overhead.UpdatedAt = DateTime.UtcNow;

        repository.Update(overhead);
        await _unitOfWork.SaveChangesAsync();

        // Obtener gastos asociados
        var expenseRepo = _unitOfWork.GetRepository<MonasteryExpense>();
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
