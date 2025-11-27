using MediatR;
using Domain.Interfaces.Repositories;

namespace Application.Features.Sales.Commands.CreateSale
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, int>
    {
        private readonly ISaleRepository _saleRepository;

        public CreateSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<int> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var saleId = await _saleRepository.CreateSaleAsync(request);
            return saleId;
        }
    }
}
