using MediatR;
using Domain.Interfaces.Repositories;

namespace Application.Features.CashSessions.Commands.ReconcileCash
{
    public class ReconcileCashCommandHandler : IRequestHandler<ReconcileCashCommand, bool>
    {
        private readonly ICashSessionRepository _cashRepo;

        public ReconcileCashCommandHandler(ICashSessionRepository cashRepo)
        {
            _cashRepo = cashRepo;
        }

        public async Task<bool> Handle(ReconcileCashCommand request, CancellationToken cancellationToken)
        {
            return await _cashRepo.ReconcileAsync(request.SessionId, request.CountedAmount);
        }
    }
}
