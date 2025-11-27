using MediatR;
using Domain.Interfaces.Repositories;

namespace Application.Features.CashSessions.Commands.AddCashMovement
{
    public class AddCashMovementCommandHandler : IRequestHandler.AddCashMovementCommand, bool  
    {
        private readonly ICashSessionRepository _cashRepo;

        public AddCashMovementCommandHandler(ICashSessionRepository cashRepo)
        {
            _cashRepo = cashRepo;
        }

        public async Task<bool> Handle(AddCashMovementCommand request, CancellationToken cancellationToken)
        {
            return await _cashRepo.AddMovementAsync(request);
        }
    }
}
