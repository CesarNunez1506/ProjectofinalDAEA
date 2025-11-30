using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Finance;
using Application.UseCases.Finance.Commands.FinancialReports;
using Application.UseCases.Finance.Queries.FinancialReports;

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
        public async Task<IActionResult> GetExpenses([FromQuery] Application.UseCases.Finance.Queries.FinancialReports.GetExpensesByPeriodQuery query)
        {
            var result = await _getExpenses.ExecuteAsync(query.StartDate, query.EndDate);
            return Ok(result);
        }
    }
}
