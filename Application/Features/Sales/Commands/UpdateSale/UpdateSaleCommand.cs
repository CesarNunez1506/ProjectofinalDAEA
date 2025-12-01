using MediatR;

namespace Application.Features.Sales.Commands.UpdateSale
{
    public class UpdateSaleCommand : IRequest<bool>
    {
        public int SaleId { get; set; }
        public decimal NewTotalAmount { get; set; }
    }
}
