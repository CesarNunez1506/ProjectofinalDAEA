using Application.DTOs.Finance;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Overheads.Queries;

/// <summary>
/// Caso de uso para obtener todos los overheads de tipo monasterio
/// </summary>
public class GetAllOverheadsUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllOverheadsUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OverheadDto>> ExecuteAsync()
    {
        var overheadRepo = _unitOfWork.GetRepository<Overhead>();
        var expenseRepo = _unitOfWork.GetRepository<MonasteryExpense>();

        var overheads = await overheadRepo.GetAsync(
            filter: o => o.Type == "monasterio",
            orderBy: q => q.OrderByDescending(o => o.Date)
        );

        var result = new List<OverheadDto>();

        foreach (var overhead in overheads)
        {
            var expenses = await expenseRepo.FindAsync(e => e.OverheadsId == overhead.Id);

            result.Add(new OverheadDto
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
            });
        }

        return result;
    }
}
