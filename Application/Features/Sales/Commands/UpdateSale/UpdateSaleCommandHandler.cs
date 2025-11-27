using MediatR;
using Domain.Interfaces.Repositories;

namespace Application.Features.Sales.Commands.UpdateSale
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, bool>
    {
        private readonly ISaleRepository _saleRepo;

        public UpdateSaleCommandHandler(ISaleRepository saleRepo)
        {
            _saleRepo = saleRepo;
        }

        public async Task<bool> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepo.GetByIdAsync(request.SaleId);

            if (sale == null)
                return false;

            sale.Observations = request.Observations;
            sale.UpdatedAt = DateTime.Now;

            await _saleRepo.UpdateAsync(sale);
            return true;
        }
    }
}
