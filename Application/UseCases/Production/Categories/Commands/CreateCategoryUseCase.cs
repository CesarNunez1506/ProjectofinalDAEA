using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Categories;

/// <summary>
/// Caso de uso para crear una nueva categoría
/// </summary>
public class CreateCategoryUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryDto> ExecuteAsync(CreateCategoryDto dto)
    {
        var categoryRepo = _unitOfWork.GetRepository<Category>();
        
        // Verificar si ya existe una categoría con el mismo nombre
        var existingCategory = await categoryRepo.FirstOrDefaultAsync(c => c.Name == dto.Name);
        if (existingCategory != null)
        {
            throw new InvalidOperationException($"Ya existe una categoría con el nombre '{dto.Name}'");
        }

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description ?? string.Empty,
            Status = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await categoryRepo.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();

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
