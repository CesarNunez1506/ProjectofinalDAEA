using MediatR;
using Domain.Entities;

namespace Application.Features.Sales.Queries.GetSalesByDateRange
{
    public class GetSalesByDateRangeQuery : IRequest<IEnumerable<Sale>>
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public GetSalesByDateRangeQuery(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}
