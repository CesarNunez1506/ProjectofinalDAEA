using MediatR;

namespace Application.Features.Sales.Commands.CancelSale
{
    public class CancelSaleCommand : IRequest<bool>
    {
        public int SaleId { get; set; }
    }
}
