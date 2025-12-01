using MediatR;

namespace Application.Features.Sales.Queries.GetDailySalesReport
{
    public class GetDailySalesReportQuery : IRequest<object>
    {
        public DateTime Date { get; set; }

        public GetDailySalesReportQuery(DateTime date)
        {
            Date = date;
        }
    }
}
