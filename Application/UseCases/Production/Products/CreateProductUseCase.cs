using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Repositories.Production;
using Domain.Interfaces.Services.Production;

namespace Application.UseCases.Production.Products;

/// <summary>
/// Caso de uso para crear un nuevo producto
/// </summary>
public class CreateProductUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IFileStorageService _fileStorageService;

    public CreateProductUseCase(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IFileStorageService fileStorageService)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<ProductDto> ExecuteAsync(CreateProductDto dto)
    {
        // Verificar que la categoría existe
        var categoryExists = await _categoryRepository.ExistsAsync(dto.CategoryId);
        if (!categoryExists)
        {
            throw new KeyNotFoundException($"La categoría con ID {dto.CategoryId} no existe");
        }

        string? imageUrl = null;

        // Manejar carga de imagen si existe
        if (dto.ImageFile != null && dto.ImageFile.Length > 0)
        {
            using var memoryStream = new MemoryStream();
            await dto.ImageFile.CopyToAsync(memoryStream);
            
            imageUrl = await _fileStorageService.SaveFileAsync(
                memoryStream.ToArray(),
                dto.ImageFile.FileName,
                "products"
            );
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            Price = dto.Price,
            Description = dto.Description ?? string.Empty,
            ImagenUrl = imageUrl,
            Status = true,
            Producible = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _productRepository.CreateAsync(product);

        return new ProductDto
        {
            Id = created.Id,
            Name = created.Name,
            CategoryId = created.CategoryId,
            Price = created.Price,
            Description = created.Description,
            ImagenUrl = created.ImagenUrl,
            Status = created.Status,
            Producible = created.Producible,
            CreatedAt = created.CreatedAt,
            UpdatedAt = created.UpdatedAt
        };
    }
}
