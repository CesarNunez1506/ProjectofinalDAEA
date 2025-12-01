using MediatR;
using Domain.Interfaces.Repositories;

namespace Application.Features.Sales.Commands.ProcessRefund
{
    public class ProcessRefundCommandHandler : IRequestHandler<ProcessRefundCommand, bool>
    {
        private readonly ISaleRepository _saleRepo;

        public ProcessRefundCommandHandler(ISaleRepository saleRepo)
        {
            _saleRepo = saleRepo;
        }

        public async Task<bool> Handle(ProcessRefundCommand request, CancellationToken cancellationToken)
        {
            return await _saleRepo.ProcessRefundAsync(request.SaleId);
        }
    }
}
