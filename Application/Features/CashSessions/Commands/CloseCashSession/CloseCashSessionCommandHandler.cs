using MediatR;
using Domain.Interfaces.Repositories;

namespace Application.Features.CashSessions.Commands.CloseCashSession
{
    public class CloseCashSessionCommandHandler : IRequestHandler<CloseCashSessionCommand, bool>
    {
        private readonly ICashSessionRepository _cashRepo;

        public CloseCashSessionCommandHandler(ICashSessionRepository cashRepo)
        {
            _cashRepo = cashRepo;
        }

        public async Task<bool> Handle(CloseCashSessionCommand request, CancellationToken cancellationToken)
        {
            return await _cashRepo.CloseSessionAsync(request.SessionId, request.ClosingAmount);
        }
    }
}
