using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Interfaces.Services.Production;

namespace Application.UseCases.Production.Products;

/// <summary>
/// Caso de uso para crear un nuevo producto
/// </summary>
public class CreateProductUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public CreateProductUseCase(
        IUnitOfWork unitOfWork,
        IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<ProductDto> ExecuteAsync(CreateProductDto dto)
    {
        var categoryRepo = _unitOfWork.GetRepository<Category>();
        var productRepo = _unitOfWork.GetRepository<Product>();
        
        // Verificar que la categoría existe
        var categoryExists = await categoryRepo.AnyAsync(c => c.Id == dto.CategoryId);
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

        await productRepo.AddAsync(product);
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
