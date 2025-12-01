using MediatR;
using Domain.Interfaces.Repositories;

namespace Application.Features.Sales.Queries.GetDailySalesReport
{
    public class GetDailySalesReportQueryHandler :
        IRequestHandler<GetDailySalesReportQuery, object>
    {
        private readonly ISaleRepository _saleRepo;

        public GetDailySalesReportQueryHandler(ISaleRepository saleRepo)
        {
            _saleRepo = saleRepo;
        }

        public async Task<object> Handle(GetDailySalesReportQuery request, CancellationToken cancellationToken)
        {
            return await _saleRepo.GetDailyReportAsync(request.Date);
        }
    }
}
