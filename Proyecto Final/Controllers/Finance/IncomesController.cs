using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Finance.Incomes.Commands;
using Application.UseCases.Finance.Incomes.Queries;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomesController : ControllerBase
    {
        private readonly CreateIncomeUseCase _createIncome;
        private readonly GetIncomesByPeriodQuery _getIncomes;

        public IncomesController(
            CreateIncomeUseCase createIncome,
            GetIncomesByPeriodQuery getIncomes)
        {
            _createIncome = createIncome;
            _getIncomes = getIncomes;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome([FromBody] CreateIncomeDto dto)
        {
            var result = await _createIncome.ExecuteAsync(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetIncomes([FromQuery] GetIncomesByPeriodQuery query)
        {
            var result = await _getIncomes.ExecuteAsync(query.StartDate, query.EndDate);
            return Ok(result);
        }
    }
}
