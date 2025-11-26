using Application.DTOs.Inventory;
using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Queries;

public record SearchCategoriesByNameQuery(string Name) : IRequest<IEnumerable<CategoryDto>>;

public class SearchCategoriesByNameQueryHandler : IRequestHandler<SearchCategoriesByNameQuery, IEnumerable<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SearchCategoriesByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> Handle(SearchCategoriesByNameQuery request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.Categories.FindAsync(c => c.Name.Contains(request.Name));
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }
}
