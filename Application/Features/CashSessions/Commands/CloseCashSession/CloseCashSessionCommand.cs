using MediatR;

namespace Application.Features.CashSessions.Commands.CloseCashSession
{
    public class CloseCashSessionCommand : IRequest<bool>
    {
        public int SessionId { get; set; }
        public decimal ClosingAmount { get; set; }
    }
}
