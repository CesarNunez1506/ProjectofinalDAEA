using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Interfaces.Services.Production;

namespace Application.UseCases.Production.Products;

/// <summary>
/// Caso de uso para actualizar un producto existente
/// </summary>
public class UpdateProductUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public UpdateProductUseCase(
        IUnitOfWork unitOfWork,
        IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<ProductDto> ExecuteAsync(Guid id, UpdateProductDto dto)
    {
        var productRepo = _unitOfWork.GetRepository<Product>();
        var categoryRepo = _unitOfWork.GetRepository<Category>();
        
        // Obtener producto con Include de Category
        var products = await productRepo.GetAsync(
            filter: p => p.Id == id,
            includeProperties: "Category"
        );
        var product = products.FirstOrDefault();
        
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
            var categoryExists = await categoryRepo.AnyAsync(c => c.Id == dto.CategoryId.Value);
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

        productRepo.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            CategoryId = product.CategoryId,
            Price = product.Price,
            Description = product.Description,
            ImagenUrl = product.ImagenUrl,
            Status = product.Status,
            Producible = product.Producible,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }
}
