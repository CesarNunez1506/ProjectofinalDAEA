using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class GeneralIncomeRepository : GenericRepository<GeneralIncome>, IGeneralIncomeRepository
{
    public GeneralIncomeRepository(AppDbContext context) : base(context)
    {
    }
}
