using Application.DTOs.Finance;
using Application.UseCases.Finance.Commands;    // ← CORREGIDO
using Application.UseCases.Finance.Queries;     // ← CORREGIDO
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Proyecto_Final.Controllers.Finanzas
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExpensesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExpenseDto dto)
        {
            var cmd = new CreateExpenseCommand(dto);
            var result = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("by-period")]
        public async Task<IActionResult> GetByPeriod([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var q = new GetExpensesByPeriodQuery(start, end);
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