using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Commands;

public record UpdateSupplierCommand(UpdateSupplierDto Dto) : IRequest<SupplierDto>;

public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, SupplierDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSupplierCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SupplierDto> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplierRepo = _unitOfWork.GetRepository<Supplier>();
        var supplier = await supplierRepo.FirstOrDefaultAsync(s => s.Id == request.Dto.Id);
        if (supplier == null)
        {
            throw new Exception($"Supplier with ID {request.Dto.Id} not found");
        }

        supplier.Ruc = request.Dto.Ruc;
        supplier.SuplierName = request.Dto.SuplierName;
        supplier.ContactName = request.Dto.ContactName;
        supplier.Email = request.Dto.Email;
        supplier.Phone = request.Dto.Phone;
        supplier.Address = request.Dto.Address;
        supplier.Status = request.Dto.Status;
        supplier.UpdatedAt = DateTime.UtcNow;

        supplierRepo.Update(supplier);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<SupplierDto>(supplier);
    }
}
