using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Finance;
using Application.UseCases.Finance.Expenses.Commands;
using Application.UseCases.Finance.Expenses.Queries;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly CreateExpenseCommand _createExpense;
        private readonly GetExpensesByPeriodQuery _getExpenses;

        public ExpensesController(
            CreateExpenseCommand createExpense,
            GetExpensesByPeriodQuery getExpenses)
        {
            _createExpense = createExpense;
            _getExpenses = getExpenses;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseCommand command)
        {
            var id = await _createExpense.ExecuteAsync(command);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetExpenses([FromQuery] GetExpensesByPeriodQuery query)
        {
            var result = await _getExpenses.ExecuteAsync(query.StartDate, query.EndDate);
            return Ok(result);
        }
    }
}
