using Domain.Entities;

namespace Domain.Interfaces;

public interface IMuseoRepository
{
    Task<Museo?> GetByIdAsync(Guid id);
    Task<IEnumerable<Museo>> GetAllAsync();
    Task<Museo> AddAsync(Museo museo);
    Task UpdateAsync(Museo museo);
    Task DeleteAsync(Museo museo);
}
