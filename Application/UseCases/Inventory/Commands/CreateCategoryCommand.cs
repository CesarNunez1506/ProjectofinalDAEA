using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Interfaces.Services;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Inventory.Commands;

public record CreateCategoryCommand(CreateCategoryDto Dto) : IRequest<CategoryDto>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request.Dto);
        category.Id = Guid.NewGuid();
        category.Status = true;
        category.CreatedAt = DateTime.UtcNow;
        category.UpdatedAt = DateTime.UtcNow;

        var categoryRepo = _unitOfWork.GetRepository<Category>();
        await categoryRepo.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CategoryDto>(category);
    }
}
