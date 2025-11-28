using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Production.Categories;

/// <summary>
/// Caso de uso para actualizar una categoría existente
/// </summary>
public class UpdateCategoryUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryDto> ExecuteAsync(Guid id, UpdateCategoryDto dto)
    {
        var categoryRepo = _unitOfWork.GetRepository<Category>();
        
        var category = await categoryRepo.GetByIdAsync(id);
        if (category == null)
        {
            throw new KeyNotFoundException($"No se encontró la categoría con ID {id}");
        }

        // Actualizar solo los campos proporcionados
        if (!string.IsNullOrEmpty(dto.Name))
        {
            // Verificar si el nuevo nombre ya existe en otra categoría
            var existingCategory = await categoryRepo.FirstOrDefaultAsync(c => c.Name == dto.Name);
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

        categoryRepo.Update(category);
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
