using MediatR;
using Domain.Interfaces.Repositories;

namespace Application.Features.Sales.Commands.CancelSale
{
    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, bool>
    {
        private readonly ISaleRepository _saleRepo;

        public CancelSaleCommandHandler(ISaleRepository saleRepo)
        {
            _saleRepo = saleRepo;
        }

        public async Task<bool> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            return await _saleRepo.CancelSaleAsync(request.SaleId);
        }
    }
}
