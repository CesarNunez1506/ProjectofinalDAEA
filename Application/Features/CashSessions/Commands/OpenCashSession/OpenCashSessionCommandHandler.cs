using MediatR;
using Domain.Interfaces.Repositories;

namespace Application.Features.CashSessions.Commands.OpenCashSession
{
    public class OpenCashSessionCommandHandler : IRequestHandler<OpenCashSessionCommand, int>
    {
        private readonly ICashSessionRepository _cashRepo;

        public OpenCashSessionCommandHandler(ICashSessionRepository cashRepo)
        {
            _cashRepo = cashRepo;
        }

        public async Task<int> Handle(OpenCashSessionCommand request, CancellationToken cancellationToken)
        {
            return await _cashRepo.OpenSessionAsync(request);
        }
    }
}
