using Domain.Interfaces.Repositories.Production;
using Domain.Interfaces.Services.Production;

namespace Application.UseCases.Production.Products;

/// <summary>
/// Caso de uso para eliminar (soft delete) un producto
/// </summary>
public class DeleteProductUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly IFileStorageService _fileStorageService;

    public DeleteProductUseCase(
        IProductRepository productRepository,
        IFileStorageService fileStorageService)
    {
        _productRepository = productRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var product = await _productRepository.GetByIdWithCategoryAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"No se encontró el producto con ID {id}");
        }

        // Opcional: eliminar imagen asociada
        // if (!string.IsNullOrEmpty(product.ImagenUrl))
        // {
        //     await _fileStorageService.DeleteFileAsync(product.ImagenUrl);
        // }

        return await _productRepository.SoftDeleteAsync(id);
    }
}
