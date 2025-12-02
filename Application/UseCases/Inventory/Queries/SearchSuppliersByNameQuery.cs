using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Queries;

public record SearchSuppliersByNameQuery(string Name) : IRequest<IEnumerable<SupplierDto>>;

public class SearchSuppliersByNameQueryHandler : IRequestHandler<SearchSuppliersByNameQuery, IEnumerable<SupplierDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SearchSuppliersByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SupplierDto>> Handle(SearchSuppliersByNameQuery request, CancellationToken cancellationToken)
    {
        var supplierRepo = _unitOfWork.GetRepository<Supplier>();
        var suppliers = await supplierRepo.FindAsync(s => s.SuplierName.Contains(request.Name));
        return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
    }
}
