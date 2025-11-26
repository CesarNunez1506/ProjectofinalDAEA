using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Commands;

public record DeleteSupplierCommand(Guid SupplierId) : IRequest<bool>;

public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSupplierCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _unitOfWork.Suppliers.FindOneAsync(s => s.Id == request.SupplierId);
        if (supplier == null)
        {
            throw new Exception($"Supplier with ID {request.SupplierId} not found");
        }

        supplier.Status = false;
        supplier.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Suppliers.UpdateAsync(supplier);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
