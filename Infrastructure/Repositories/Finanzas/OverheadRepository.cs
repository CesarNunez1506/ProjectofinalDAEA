using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class OverheadRepository : GenericRepository<Overhead>, IOverheadRepository
{
    public OverheadRepository(AppDbContext context) : base(context)
    {
    }
}
