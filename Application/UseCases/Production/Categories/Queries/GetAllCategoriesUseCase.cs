using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Categories;

/// <summary>
/// Caso de uso para obtener todas las categorías
/// </summary>
public class GetAllCategoriesUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCategoriesUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CategoryDto>> ExecuteAsync()
    {
        var categoryRepo = _unitOfWork.GetRepository<Category>();
        var categories = await categoryRepo.GetAllAsync();

        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Status = c.Status,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        });
    }
}
