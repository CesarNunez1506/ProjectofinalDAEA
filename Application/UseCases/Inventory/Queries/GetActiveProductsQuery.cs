using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Queries;

public record GetActiveProductsQuery : IRequest<IEnumerable<ProductDto>>;

public class GetActiveProductsQueryHandler : IRequestHandler<GetActiveProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetActiveProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetActiveProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.FindAsync(p => p.Status == true);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}
