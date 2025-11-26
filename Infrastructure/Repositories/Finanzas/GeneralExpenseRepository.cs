using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class GeneralExpenseRepository : GenericRepository<GeneralExpense>, IGeneralExpenseRepository
{
    public GeneralExpenseRepository(AppDbContext context) : base(context)
    {
    }
}
