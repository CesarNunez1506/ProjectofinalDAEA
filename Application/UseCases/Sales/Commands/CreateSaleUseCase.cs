using Application.DTOs.Sales;
using Domain.Entities;
using Domain.Interfaces.Repositories.Sales;

namespace Application.UseCases.Sales;

/// <summary>
/// Caso de uso para crear una nueva venta con sus detalles.
/// </summary>
public class CreateSaleUseCase
{
    private readonly ISaleRepository _saleRepository;
    private readonly IStoreRepository _storeRepository;
    private readonly ICustomerRepository _customerRepository;

    public CreateSaleUseCase(
        ISaleRepository saleRepository,
        IStoreRepository storeRepository,
        ICustomerRepository customerRepository)
    {
        _saleRepository = saleRepository;
        _storeRepository = storeRepository;
        _customerRepository = customerRepository;
    }

    public async Task<SaleDto> ExecuteAsync(CreateSaleDto dto)
    {
        // Validar tienda
        var store = await _storeRepository.GetByIdAsync(dto.StoreId);
        if (store == null)
            throw new InvalidOperationException("La tienda especificada no existe");

        // Validar cliente
        var customer = await _customerRepository.GetByIdAsync(dto.CustomerId);
        if (customer == null)
            throw new InvalidOperationException("El cliente especificado no existe");

        // Crear entidad venta
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            StoreId = dto.StoreId,
            CustomerId = dto.CustomerId,
            TotalAmount = dto.Details.Sum(d => d.Quantity * d.UnitPrice),
            Status = "COMPLETED",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Details = dto.Details.Select(d => new SaleDetail
            {
                Id = Guid.NewGuid(),
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice
            }).ToList()
        };

        var created = await _saleRepository.CreateAsync(sale);

        return new SaleDto
        {
            Id = created.Id,
            StoreId = created.StoreId,
            CustomerId = created.CustomerId,
            TotalAmount = created.TotalAmount,
            Status = created.Status,
            CreatedAt = created.CreatedAt
        };
    }
}
