using Application.UseCases.Finance.Overheads.Commands;
using Application.DTOs.Finance;

using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Overheads.Commands

{
/// <summary>
/// Caso de uso para crear un nuevo overhead (cierre de gastos del monasterio)
/// Asocia automáticamente los gastos sin asignar al nuevo overhead
/// </summary>
public class CreateOverheadUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOverheadUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OverheadDto> ExecuteAsync(CreateOverheadDto dto)
    {
        var overheadRepo = _unitOfWork.GetRepository<Overhead>();
        var expenseRepo = _unitOfWork.GetRepository<MonasteryExpense>();

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            // Crear el overhead con tipo "monasterio"
            var overhead = new Overhead
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Date = DateTime.SpecifyKind(dto.Date, DateTimeKind.Utc),
                Type = "monasterio",
                Amount = dto.Amount,
                Description = dto.Description,
                Status = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await overheadRepo.AddAsync(overhead);
            await _unitOfWork.SaveChangesAsync();

            // Obtener gastos sin asignar (OverheadsId == null)
            var unassignedExpenses = await expenseRepo.FindAsync(e => e.OverheadsId == null);

            // Asociar los gastos al overhead creado
            foreach (var expense in unassignedExpenses)
            {
                expense.OverheadsId = overhead.Id;
                expense.UpdatedAt = DateTime.UtcNow;
                expenseRepo.Update(expense);
            }

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

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
                MonasteryExpenses = unassignedExpenses.Select(e => new MonasteryExpenseDto
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
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
}
