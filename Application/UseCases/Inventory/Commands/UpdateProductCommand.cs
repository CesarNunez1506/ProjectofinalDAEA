using Application.DTOs.Inventory;
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
        var productRepo = _unitOfWork.GetRepository<Product>();
        var product = await productRepo.FirstOrDefaultAsync(p => p.Id == request.Dto.Id);
        if (product == null)
        {
            throw new ProductNotFoundException(request.Dto.Id);
        }

        // Validar que la categoría exista (si se proporciona)
        if (request.Dto.CategoryId.HasValue)
        {
            var categoryRepo = _unitOfWork.GetRepository<Category>();
            var categoryExists = await categoryRepo.ExistsAsync(c => c.Id == request.Dto.CategoryId.Value);
            if (!categoryExists)
            {
                throw new CategoryNotFoundException(request.Dto.CategoryId.Value);
            }
        }

        // Actualizar propiedades
        if (request.Dto.Name != null) product.Name = request.Dto.Name;
        if (request.Dto.CategoryId.HasValue) product.CategoryId = request.Dto.CategoryId.Value;
        if (request.Dto.Price.HasValue) product.Price = request.Dto.Price.Value;
        if (request.Dto.Description != null) product.Description = request.Dto.Description;
        if (request.Dto.ImagenUrl != null) product.ImagenUrl = request.Dto.ImagenUrl;
        if (request.Dto.Status.HasValue) product.Status = request.Dto.Status.Value;
        if (request.Dto.Producible.HasValue) product.Producible = request.Dto.Producible.Value;
        product.UpdatedAt = DateTime.UtcNow;

        productRepo.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductDto>(product);
    }
}
