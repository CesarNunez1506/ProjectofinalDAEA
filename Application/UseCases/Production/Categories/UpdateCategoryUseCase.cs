using Application.DTOs.Production;
using Domain.Interfaces.Repositories.Production;

namespace Application.UseCases.Production.Categories;

/// <summary>
/// Caso de uso para actualizar una categoría existente
/// </summary>
public class UpdateCategoryUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryUseCase(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> ExecuteAsync(Guid id, UpdateCategoryDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            throw new KeyNotFoundException($"No se encontró la categoría con ID {id}");
        }

        // Actualizar solo los campos proporcionados
        if (!string.IsNullOrEmpty(dto.Name))
        {
            // Verificar si el nuevo nombre ya existe en otra categoría
            var existingCategory = await _categoryRepository.GetByNameAsync(dto.Name);
            if (existingCategory != null && existingCategory.Id != id)
            {
                throw new InvalidOperationException($"Ya existe otra categoría con el nombre '{dto.Name}'");
            }
            category.Name = dto.Name;
        }

        if (dto.Description != null)
        {
            category.Description = dto.Description;
        }

        if (dto.Status.HasValue)
        {
            category.Status = dto.Status.Value;
        }

        category.UpdatedAt = DateTime.UtcNow;

        var updated = await _categoryRepository.UpdateAsync(category);

        return new CategoryDto
        {
            Id = updated.Id,
            Name = updated.Name,
            Description = updated.Description,
            Status = updated.Status,
            CreatedAt = updated.CreatedAt,
            UpdatedAt = updated.UpdatedAt
        };
    }
}
