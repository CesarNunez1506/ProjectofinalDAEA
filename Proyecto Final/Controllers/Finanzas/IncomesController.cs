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
    public class IncomesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IncomesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IncomeDto dto)
        {
            var cmd = new CreateIncomeCommand(dto);
            var result = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("by-period")]
        public async Task<IActionResult> GetByPeriod([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var q = new GetIncomesByPeriodQuery(start, end);
            var result = await _mediator.Send(q);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok();
        }
    }
}
