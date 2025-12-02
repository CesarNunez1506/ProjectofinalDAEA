using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Commands;

public record UpdateCategoryCommand(UpdateCategoryDto Dto) : IRequest<CategoryDto>;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryRepo = _unitOfWork.GetRepository<Category>();
        var category = await categoryRepo.FirstOrDefaultAsync(c => c.Id == request.Dto.Id);
        if (category == null)
        {
            throw new Exception($"Category with ID {request.Dto.Id} not found");
        }

        category.Name = request.Dto.Name;
        category.Description = request.Dto.Description;
        category.Status = request.Dto.Status;
        category.UpdatedAt = DateTime.UtcNow;

        categoryRepo.Update(category);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CategoryDto>(category);
    }
}
