using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Categories;

/// <summary>
/// Caso de uso para obtener una categoría por ID
/// </summary>
public class GetCategoryByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoryByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryDto?> ExecuteAsync(Guid id)
    {
        var categoryRepo = _unitOfWork.GetRepository<Category>();
        var category = await categoryRepo.GetByIdAsync(id);
        
        if (category == null)
            return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Status = category.Status,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };
    }
}
