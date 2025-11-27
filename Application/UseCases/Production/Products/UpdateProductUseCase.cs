using Application.DTOs.Production;
using Domain.Interfaces.Repositories.Production;
using Domain.Interfaces.Services.Production;

namespace Application.UseCases.Production.Products;

/// <summary>
/// Caso de uso para actualizar un producto existente
/// </summary>
public class UpdateProductUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IFileStorageService _fileStorageService;

    public UpdateProductUseCase(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IFileStorageService fileStorageService)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<ProductDto> ExecuteAsync(Guid id, UpdateProductDto dto)
    {
        var product = await _productRepository.GetByIdWithCategoryAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"No se encontró el producto con ID {id}");
        }

        // Actualizar solo los campos proporcionados
        if (!string.IsNullOrEmpty(dto.Name))
        {
            product.Name = dto.Name;
        }

        if (dto.CategoryId.HasValue)
        {
            // Verificar que la nueva categoría existe
            var categoryExists = await _categoryRepository.ExistsAsync(dto.CategoryId.Value);
            if (!categoryExists)
            {
                throw new InvalidOperationException($"La categoría con ID {dto.CategoryId.Value} no existe");
            }
            product.CategoryId = dto.CategoryId.Value;
        }

        if (dto.Price.HasValue)
        {
            product.Price = dto.Price.Value;
        }

        if (dto.Description != null)
        {
            product.Description = dto.Description;
        }

        if (dto.Status.HasValue)
        {
            product.Status = dto.Status.Value;
        }

        // Manejar actualización de imagen
        if (dto.ImageFile != null && dto.ImageFile.Length > 0)
        {
            // Eliminar imagen anterior si existe
            if (!string.IsNullOrEmpty(product.ImagenUrl))
            {
                await _fileStorageService.DeleteFileAsync(product.ImagenUrl);
            }

            // Guardar nueva imagen
            using var memoryStream = new MemoryStream();
            await dto.ImageFile.CopyToAsync(memoryStream);
            
            product.ImagenUrl = await _fileStorageService.SaveFileAsync(
                memoryStream.ToArray(),
                dto.ImageFile.FileName,
                "products"
            );
        }

        product.UpdatedAt = DateTime.UtcNow;

        var updated = await _productRepository.UpdateAsync(product);

        return new ProductDto
        {
            Id = updated.Id,
            Name = updated.Name,
            CategoryId = updated.CategoryId,
            Price = updated.Price,
            Description = updated.Description,
            ImagenUrl = updated.ImagenUrl,
            Status = updated.Status,
            Producible = updated.Producible,
            CreatedAt = updated.CreatedAt,
            UpdatedAt = updated.UpdatedAt
        };
    }
}
