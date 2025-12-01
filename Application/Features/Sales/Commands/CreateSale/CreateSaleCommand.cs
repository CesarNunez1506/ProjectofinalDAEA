using MediatR;

namespace Application.Features.Sales.Commands.CreateSale
{
    public class CreateSaleCommand : IRequest<int>
    {
        public int StoreId { get; set; }
        public int CustomerId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal TotalAmount { get; set; }

        public List<SaleDetailDto> Details { get; set; } = new();
    }

    public class SaleDetailDto
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
