using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Finance;
using Application.UseCases.Finance.Commands.FinancialReports;
using Application.UseCases.Finance.Queries.FinancialReports;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomesController : ControllerBase
    {
        private readonly CreateIncomeCommand _createIncome;
        private readonly GetIncomesByPeriodQuery _getIncomes;

        public IncomesController(
            CreateIncomeCommand createIncome,
            GetIncomesByPeriodQuery getIncomes)
        {
            _createIncome = createIncome;
            _getIncomes = getIncomes;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome([FromBody] CreateIncomeCommand command)
        {
            var id = await _createIncome.ExecuteAsync(command);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetIncomes([FromQuery] Application.UseCases.Finance.Queries.FinancialReports.GetIncomesByPeriodQuery query)
        {
            var result = await _getIncomes.ExecuteAsync(query.StartDate, query.EndDate);
            return Ok(result);
        }
    }
}
