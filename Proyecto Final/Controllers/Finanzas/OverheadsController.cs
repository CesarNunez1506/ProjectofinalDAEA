using Application.DTOs.Finance;
using Application.Features.Finance.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Proyecto_Final.Controllers.Finanzas
{
    [ApiController]
    [Route("api/[controller]")]
    public class OverheadsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OverheadsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OverheadDto dto)
        {
            var cmd = new RecordOverheadCommand(dto);
            var result = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(System.Guid id)
        {
            return Ok();
        }
    }
}
