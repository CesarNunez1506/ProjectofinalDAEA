using MediatR;
using Domain.Interfaces.Repositories;

namespace Application.Features.CashSessions.Queries.GetCashSessionById
{
    public class GetCashSessionByIdQueryHandler :
        IRequestHandler<GetCashSessionByIdQuery, CashSession?>
    {
        private readonly ICashSessionRepository _repo;

        public GetCashSessionByIdQueryHandler(ICashSessionRepository repo)
        {
            _repo = repo;
        }

        public async Task<CashSession?> Handle(GetCashSessionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.SessionId);
        }
    }
}
