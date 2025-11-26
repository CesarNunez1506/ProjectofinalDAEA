using Application.DTOs.Finance;
using Application.Features.Finance.Commands;
using Application.Features.Finance.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Proyecto_Final.Controllers.Finanzas
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinancialReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody] GenerateFinancialReportCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("by-period")]
        public async Task<IActionResult> GetByPeriod([FromQuery] DateTime start, [FromQuery] DateTime? end)
        {
            var q = new GetFinancialReportByDateQuery(start, end);
            var result = await _mediator.Send(q);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("profit-loss")]
        public async Task<IActionResult> ProfitLoss([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var q = new GetProfitLossStatementQuery(start, end);
            var result = await _mediator.Send(q);
            return Ok(result);
        }
    }
}
