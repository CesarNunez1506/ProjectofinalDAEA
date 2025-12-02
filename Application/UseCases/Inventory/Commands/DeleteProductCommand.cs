using Domain.Entities;
using Domain.Exceptions.Inventory;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Commands;

/// <summary>
/// Command para eliminar (soft delete) un producto
/// </summary>
public record DeleteProductCommand(Guid ProductId) : IRequest<bool>;

/// <summary>
/// Handler para eliminar un producto
/// </summary>
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productRepo = _unitOfWork.GetRepository<Product>();
        var product = await productRepo.FirstOrDefaultAsync(p => p.Id == request.ProductId);
        if (product == null)
        {
            throw new ProductNotFoundException(request.ProductId);
        }

        // Soft delete: cambiar status a false
        product.Status = false;
        product.UpdatedAt = DateTime.UtcNow;

        productRepo.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
