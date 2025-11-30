using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Finance;
using Application.UseCases.Finance.Commands.FinancialReports;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OverheadsController : ControllerBase
    {
        private readonly RecordOverheadCommand _recordOverhead;

        public OverheadsController(RecordOverheadCommand recordOverhead)
        {
            _recordOverhead = recordOverhead;
        }

        [HttpPost]
        public async Task<IActionResult> RecordOverhead([FromBody] RecordOverheadCommand command)
        {
            var id = await _recordOverhead.ExecuteAsync(command);
            return Ok(id);
        }
    }
}
