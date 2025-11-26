using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Queries;

public record GetProducibleProductsQuery : IRequest<IEnumerable<ProductDto>>;

public class GetProducibleProductsQueryHandler : IRequestHandler<GetProducibleProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProducibleProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProducibleProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.FindAsync(p => p.Producible == true && p.Status == true);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}
