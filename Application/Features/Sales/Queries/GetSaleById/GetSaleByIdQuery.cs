using MediatR;
using Domain.Entities;

namespace Application.Features.Sales.Queries.GetSaleById
{
    public class GetSaleByIdQuery : IRequest<Sale?>
    {
        public Guid SaleId { get; set; }

        public GetSaleByIdQuery(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
