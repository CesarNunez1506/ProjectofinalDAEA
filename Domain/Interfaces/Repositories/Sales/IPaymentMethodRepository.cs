using Domain.Entities;

namespace Domain.Repositories.Sales
{
    public interface IPaymentMethodRepository
    {
        Task<IEnumerable<PaymentMethod>> GetAllAsync();
        Task<PaymentMethod?> GetByIdAsync(Guid id);
        Task AddAsync(PaymentMethod method);
        Task UpdateAsync(PaymentMethod method);
        Task DeleteAsync(Guid id);
    }
}
