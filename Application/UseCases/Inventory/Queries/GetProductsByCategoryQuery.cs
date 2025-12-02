using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Queries;

public record GetProductsByCategoryQuery(Guid CategoryId) : IRequest<IEnumerable<ProductDto>>;

public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, IEnumerable<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductsByCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var productRepo = _unitOfWork.GetRepository<Product>();
        var products = await productRepo.FindAsync(p => p.CategoryId == request.CategoryId && p.Status == true);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}
