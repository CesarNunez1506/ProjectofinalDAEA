using MediatR;

namespace Application.Features.CashSessions.Commands.ReconcileCash
{
    public class ReconcileCashCommand : IRequest<bool>
    {
        public int SessionId { get; set; }
        public decimal CountedAmount { get; set; }
    }
}
