using Domain.Entities;
using Domain.Repositories.Sales;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Sales
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly AppDbContext _context;

        public PaymentMethodRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentMethod>> GetAllAsync()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        public async Task<PaymentMethod?> GetByIdAsync(Guid id)
        {
            return await _context.PaymentMethods.FindAsync(id);
        }

        public async Task AddAsync(PaymentMethod method)
        {
            await _context.PaymentMethods.AddAsync(method);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PaymentMethod method)
        {
            _context.PaymentMethods.Update(method);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.PaymentMethods.FindAsync(id);
            if (entity != null)
            {
                _context.PaymentMethods.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
