using MediatR;
using Domain.Interfaces.Repositories;

namespace Application.Features.Sales.Commands.ProcessRefund
{
    public class ProcessRefundCommandHandler : IRequestHandler<ProcessRefundCommand, bool>
    {
        private readonly ISaleRepository _saleRepository;

        public ProcessRefundCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<bool> Handle(ProcessRefundCommand request, CancellationToken cancellationToken)
        {
            return await _saleRepository.ProcessRefundAsync(request.SaleId, request.RefundAmount);
        }
    }
}
