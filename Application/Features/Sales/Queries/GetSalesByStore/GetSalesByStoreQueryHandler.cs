using MediatR;
using Domain.Interfaces.Repositories;

namespace Application.Features.Sales.Queries.GetSalesByStore
{
    public class GetSalesByStoreQueryHandler :
        IRequestHandler<GetSalesByStoreQuery, IEnumerable<Sale>>
    {
        private readonly ISaleRepository _saleRepo;

        public GetSalesByStoreQueryHandler(ISaleRepository saleRepo)
        {
            _saleRepo = saleRepo;
        }

        public async Task<IEnumerable<Sale>> Handle(GetSalesByStoreQuery request, CancellationToken cancellationToken)
        {
            return await _saleRepo.GetByStoreAsync(request.StoreId);
        }
    }
}
