using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Categories;

/// <summary>
/// Caso de uso para crear una nueva categoría
/// </summary>
public class CreateCategoryUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryUseCase(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> ExecuteAsync(CreateCategoryDto dto)
    {
        // Verificar si ya existe una categoría con el mismo nombre
        var existingCategory = await _categoryRepository.GetByNameAsync(dto.Name);
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

        var created = await _categoryRepository.CreateAsync(category);

        return new CategoryDto
        {
            Id = created.Id,
            Name = created.Name,
            Description = created.Description,
            Status = created.Status,
            CreatedAt = created.CreatedAt,
            UpdatedAt = created.UpdatedAt
        };
    }
}
