using MediatR;
using Domain.Interfaces.Repositories;

namespace Application.Features.Sales.Commands.UpdateSale
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, bool>
    {
        private readonly ISaleRepository _saleRepository;

        public UpdateSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<bool> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            return await _saleRepository.UpdateSaleAsync(request.SaleId, request.NewTotalAmount);
        }
    }
}
