using MediatR;
using Domain.Interfaces.Repositories;

namespace Application.Features.Sales.Queries.GetSaleById
{
    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, Sale?>
    {
        private readonly ISaleRepository _saleRepo;

        public GetSaleByIdQueryHandler(ISaleRepository saleRepo)
        {
            _saleRepo = saleRepo;
        }

        public async Task<Sale?> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _saleRepo.GetByIdAsync(request.SaleId);
        }
    }
}
