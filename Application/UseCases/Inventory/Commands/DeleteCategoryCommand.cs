using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Commands;

public record DeleteCategoryCommand(Guid CategoryId) : IRequest<bool>;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories.FindOneAsync(c => c.Id == request.CategoryId);
        if (category == null)
        {
            throw new Exception($"Category with ID {request.CategoryId} not found");
        }

        category.Status = false;
        category.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Categories.UpdateAsync(category);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
