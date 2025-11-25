using Application.DTOs.Inventory;
using AutoMapper;
using Domain.Exceptions.Inventory;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Inventory.Commands;

/// <summary>
/// Command para actualizar un producto
/// </summary>
public record UpdateProductCommand(UpdateProductDto Dto) : IRequest<ProductDto>;

/// <summary>
/// Handler para actualizar un producto
/// </summary>
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // Validar que el producto exista
        var product = await _unitOfWork.Products.GetByIdAsync((int)request.Dto.Id);
        if (product == null)
        {
            throw new ProductNotFoundException(request.Dto.Id);
        }

        // Validar que la categoría exista
        var categoryExists = await _unitOfWork.Categories.ExistsAsync(request.Dto.CategoryId);
        if (!categoryExists)
        {
            throw new CategoryNotFoundException(request.Dto.CategoryId);
        }

        // Actualizar propiedades
        product.Name = request.Dto.Name;
        product.CategoryId = request.Dto.CategoryId;
        product.Price = request.Dto.Price;
        product.Description = request.Dto.Description;
        product.ImagenUrl = request.Dto.ImagenUrl;
        product.Status = request.Dto.Status;
        product.Producible = request.Dto.Producible;
        product.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Products.UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductDto>(product);
    }
}
