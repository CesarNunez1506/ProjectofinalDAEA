using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Queries;

public record SearchProductsByNameQuery(string Name) : IRequest<IEnumerable<ProductDto>>;

public class SearchProductsByNameQueryHandler : IRequestHandler<SearchProductsByNameQuery, IEnumerable<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SearchProductsByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(SearchProductsByNameQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.FindAsync(p => p.Name.Contains(request.Name));
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}
