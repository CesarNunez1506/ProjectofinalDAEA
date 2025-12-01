using MediatR;

namespace Application.Features.CashSessions.Commands.AddCashMovement
{
    public class AddCashMovementCommand : IRequest<bool>
    {
        public int SessionId { get; set; }
        public decimal Amount { get; set; }
        public string MovementType { get; set; } = ""; // "IN" o "OUT"
        public string? Description { get; set; }
    }
}
