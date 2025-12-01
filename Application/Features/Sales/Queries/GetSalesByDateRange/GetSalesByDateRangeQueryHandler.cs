using MediatR;
using Domain.Interfaces.Repositories;

namespace Application.Features.Sales.Queries.GetSalesByDateRange
{
    public class GetSalesByDateRangeQueryHandler :
        IRequestHandler<GetSalesByDateRangeQuery, IEnumerable<Sale>>
    {
        private readonly ISaleRepository _saleRepo;

        public GetSalesByDateRangeQueryHandler(ISaleRepository saleRepo)
        {
            _saleRepo = saleRepo;
        }

        public async Task<IEnumerable<Sale>> Handle(GetSalesByDateRangeQuery request, CancellationToken cancellationToken)
        {
            return await _saleRepo.GetByDateRangeAsync(request.Start, request.End);
        }
    }
}
