using MediatR;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Features.Sales.Commands.CreateSale
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _saleRepo;
        private readonly IProductRepository _productRepo;

        public CreateSaleCommandHandler(ISaleRepository saleRepo, IProductRepository productRepo)
        {
            _saleRepo = saleRepo;
            _productRepo = productRepo;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            double total = 0;

            // 1️⃣ Verificar stock y calcular totales
            foreach (var detail in request.Details)
            {
                var product = await _productRepo.GetByIdAsync(detail.ProductId);

                if (product == null)
                    throw new Exception($"El producto {detail.ProductId} no existe.");

                if (product.Stock < detail.Quantity)
                    throw new Exception($"Stock insuficiente para {product.ProductName}");

                // Calcular total por producto
                total += detail.Quantity * detail.Mount;

                // Descontar stock
                product.Stock -= detail.Quantity;
                await _productRepo.UpdateAsync(product);
            }

            // 2️⃣ Crear entidad Sale
            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                StoreId = request.StoreId,
                TotalIncome = total,
                IncomeDate = DateTime.Now.ToString("yyyy-MM-dd"),
                Observations = request.Observations,
                CreatedAt = DateTime.Now
            };

            // 3️⃣ Guardar venta con detalles
            await _saleRepo.AddSaleWithDetailsAsync(sale, request.Details);

            return sale.Id;
        }
    }
}
