using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Queries;

public record GetAllSuppliersQuery : IRequest<IEnumerable<SupplierDto>>;

public class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQuery, IEnumerable<SupplierDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllSuppliersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SupplierDto>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
    {
        var suppliers = await _unitOfWork.Suppliers.GetAllAsync();
        return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
    }
}
