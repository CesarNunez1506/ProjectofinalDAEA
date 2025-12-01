using MediatR;

namespace Application.Features.CashSessions.Commands.OpenCashSession
{
    public class OpenCashSessionCommand : IRequest<int>
    {
        public int StoreId { get; set; }
        public int UserId { get; set; }
        public decimal InitialAmount { get; set; }
    }
}
