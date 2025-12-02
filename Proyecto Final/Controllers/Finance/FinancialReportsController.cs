using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Finance.FinancialReports.Commands;
using Application.UseCases.Finance.FinancialReports.Queries;
using System.Globalization;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialReportsController : ControllerBase
    {
        private readonly GenerateFinancialReportUseCase _generateReport;
        private readonly GetFinancialReportByDateQuery _getReport;
        private readonly GetProfitLossStatementQuery _getProfitLoss;

        public FinancialReportsController(
            GenerateFinancialReportUseCase generateReport,
            GetFinancialReportByDateQuery getReport,
            GetProfitLossStatementQuery getProfitLoss)
        {
            _generateReport = generateReport;
            _getReport = getReport;
            _getProfitLoss = getProfitLoss;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateReport([FromBody] GenerateFinancialReportDto dto)
        {
            var report = await _generateReport.ExecuteAsync(dto);
            return Ok(report);
        }

        /// <summary>
        /// Obtiene el reporte financiero para la fecha especificada (ruta: /api/FinancialReports/{date}).
        /// Convierte la fecha a UTC antes de enviarla al UseCase para evitar errores con PostgreSQL.
        /// </summary>
        /// <param name="date">Fecha del reporte en formato ISO (yyyy-MM-dd).</param>
        [HttpGet("{date}")]
        public async Task<IActionResult> GetReport([FromRoute] string date)
        {
            // Parseamos el string de la ruta a DateTime con formato yyyy-MM-dd
            if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var parsedDate))
            {
                return BadRequest("Formato de fecha inválido. Use yyyy-MM-dd.");
            }

            // Convertimos explícitamente a UTC
            var dateUtc = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);

            var result = await _getReport.ExecuteAsync(dateUtc);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("profit-loss")]
        public async Task<IActionResult> GetProfitLoss([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            // Convertimos a UTC antes de enviar al UseCase
            var startUtc = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
            var endUtc = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

            var result = await _getProfitLoss.ExecuteAsync(startUtc, endUtc);
            return Ok(result);
        }
    }
}
