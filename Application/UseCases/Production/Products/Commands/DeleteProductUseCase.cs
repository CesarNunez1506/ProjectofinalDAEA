using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Interfaces.Services.Production;

namespace Application.UseCases.Production.Products;

/// <summary>
/// Caso de uso para eliminar (soft delete) un producto
/// </summary>
public class DeleteProductUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public DeleteProductUseCase(
        IUnitOfWork unitOfWork,
        IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var productRepo = _unitOfWork.GetRepository<Product>();
        
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

        // Opcional: eliminar imagen asociada
        // if (!string.IsNullOrEmpty(product.ImagenUrl))
        // {
        //     await _fileStorageService.DeleteFileAsync(product.ImagenUrl);
        // }

        product.Status = false;
        product.UpdatedAt = DateTime.UtcNow;
        productRepo.Update(product);
        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }
}
