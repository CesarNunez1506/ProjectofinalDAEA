using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Exceptions.Inventory;
using Domain.Interfaces.Services;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Inventory.Commands;

public record CreateSupplierCommand(CreateSupplierDto Dto) : IRequest<SupplierDto>;

public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, SupplierDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSupplierCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SupplierDto> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        // Validar que no exista un proveedor con el mismo RUC
        var exists = await _unitOfWork.Suppliers.ExistsAsync(s => s.Ruc == request.Dto.Ruc);
        if (exists)
        {
            throw new DuplicateSupplierException(request.Dto.Ruc);
        }

        var supplier = _mapper.Map<Supplier>(request.Dto);
        supplier.Id = Guid.NewGuid();
        supplier.Status = true;
        supplier.CreatedAt = DateTime.UtcNow;
        supplier.UpdatedAt = DateTime.UtcNow;

        var createdSupplier = await _unitOfWork.Suppliers.AddAsync(supplier);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<SupplierDto>(createdSupplier);
    }
}
