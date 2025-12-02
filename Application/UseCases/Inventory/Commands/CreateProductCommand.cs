using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions.Inventory;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Commands;

/// <summary>
/// Command para crear un nuevo producto
/// </summary>
public record CreateProductCommand(CreateProductDto Dto) : IRequest<ProductDto>;

/// <summary>
/// Handler para crear un producto
/// </summary>
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Validar que la categoría exista
        var categoryRepo = _unitOfWork.GetRepository<Category>();
        var categoryExists = await categoryRepo.AnyAsync(c => c.Id == request.Dto.CategoryId);
        if (!categoryExists)
        {
            throw new CategoryNotFoundException(request.Dto.CategoryId);
        }

        // Mapear DTO a entidad
        var product = _mapper.Map<Product>(request.Dto);
        product.Id = Guid.NewGuid();
        product.Status = true;
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;

        // Guardar en el repositorio
        var productRepo = _unitOfWork.GetRepository<Product>();
        await productRepo.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        // Retornar DTO
        return _mapper.Map<ProductDto>(product);
    }
}
