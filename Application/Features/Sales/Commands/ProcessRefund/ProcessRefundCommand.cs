using MediatR;

namespace Application.Features.Sales.Commands.ProcessRefund
{
    public class ProcessRefundCommand : IRequest<bool>
    {
        public int SaleId { get; set; }
        public decimal RefundAmount { get; set; }
    }
}
