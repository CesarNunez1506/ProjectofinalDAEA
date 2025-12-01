using MediatR;
using Domain.Entities;

namespace Application.Features.CashSessions.Queries.GetCashSessionById
{
    public class GetCashSessionByIdQuery : IRequest<CashSession?>
    {
        public int SessionId { get; set; }

        public GetCashSessionByIdQuery(int id)
        {
            SessionId = id;
        }
    }
}
