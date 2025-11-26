using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Queries;

public record GetSupplierByRucQuery(long Ruc) : IRequest<SupplierDto?>;

public class GetSupplierByRucQueryHandler : IRequestHandler<GetSupplierByRucQuery, SupplierDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSupplierByRucQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SupplierDto?> Handle(GetSupplierByRucQuery request, CancellationToken cancellationToken)
    {
        var supplier = await _unitOfWork.Suppliers.FindOneAsync(s => s.Ruc == request.Ruc);
        return supplier == null ? null : _mapper.Map<SupplierDto>(supplier);
    }
}
