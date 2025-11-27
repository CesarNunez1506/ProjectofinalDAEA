using MediatR;
using Domain.Interfaces.Repositories;

namespace Application.Features.Sales.Commands.CancelSale
{
    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, bool>
    {
        private readonly ISaleRepository _saleRepository;

        public CancelSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<bool> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            return await _saleRepository.CancelSaleAsync(request.SaleId);
        }
    }
}
