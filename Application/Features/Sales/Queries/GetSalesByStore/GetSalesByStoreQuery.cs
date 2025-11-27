using MediatR;
using Domain.Entities;

namespace Application.Features.Sales.Queries.GetSalesByStore
{
    public class GetSalesByStoreQuery : IRequest<IEnumerable<Sale>>
    {
        public Guid StoreId { get; set; }

        public GetSalesByStoreQuery(Guid storeId)
        {
            StoreId = storeId;
        }
    }
}
