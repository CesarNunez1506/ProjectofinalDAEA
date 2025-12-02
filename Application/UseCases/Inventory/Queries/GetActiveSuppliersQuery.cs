using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Queries;

public record GetActiveSuppliersQuery : IRequest<IEnumerable<SupplierDto>>;

public class GetActiveSuppliersQueryHandler : IRequestHandler<GetActiveSuppliersQuery, IEnumerable<SupplierDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetActiveSuppliersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SupplierDto>> Handle(GetActiveSuppliersQuery request, CancellationToken cancellationToken)
    {
        var supplierRepo = _unitOfWork.GetRepository<Supplier>();
        var suppliers = await supplierRepo.FindAsync(s => s.Status == true);
        return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
    }
}
