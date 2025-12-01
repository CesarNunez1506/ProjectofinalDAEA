using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application.UseCases.Finance.Commands
{
    /// <summary>
    /// Caso de uso para eliminar un overhead
    /// </summary>
    public class DeleteOverheadUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOverheadUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(Guid id)
        {
            var repository = _unitOfWork.GetRepository<Overhead>();

            var overhead = await repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No se encontró el overhead con ID {id}");

            repository.Remove(overhead);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
