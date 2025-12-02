using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Application.DTOs.Finance;
using Application.UseCases.Finance.FinancialReports.Commands;
using Application.UseCases.Finance.Expenses.Queries;
using Application.UseCases.Finance.Incomes.Queries;

namespace ProyectoFinal.Controllers.Finance
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialReportsExportController : ControllerBase
    {
        private readonly GetIncomesByPeriodQuery _getIncomes;
        private readonly GetExpensesByPeriodQuery _getExpenses;

        public FinancialReportsExportController(
            GetIncomesByPeriodQuery getIncomes,
            GetExpensesByPeriodQuery getExpenses)
        {
            _getIncomes = getIncomes;
            _getExpenses = getExpenses;
        }

        /// <summary>
        /// Genera un archivo Excel (.xlsx) con los ingresos y gastos del periodo.
        /// Recibe un objeto similar a GenerateFinancialReportCommand en el body con StartDate/EndDate.
        /// </summary>
        [HttpPost("export")]
        public async Task<IActionResult> ExportReport([FromBody] GenerateFinancialReportDto command)
        {
            if (command == null)
                return BadRequest("Request body is required.");

            var start = command.StartDate;
            var end = command.EndDate;

            // Obtener datos desde los use-cases existentes
            var incomes = (await _getIncomes.ExecuteAsync(start, end)) ?? new List<IncomeDto>();
            var expenses = (await _getExpenses.ExecuteAsync(start, end)) ?? new List<ExpenseDto>();

            // Crear workbook con ClosedXML
            using var workbook = new XLWorkbook();

            // Hoja de Ingresos
            var wsIn = workbook.Worksheets.Add("Incomes");
            wsIn.Cell(1, 1).Value = "Id";
            wsIn.Cell(1, 2).Value = "ModuleId";
            wsIn.Cell(1, 3).Value = "IncomeType";
            wsIn.Cell(1, 4).Value = "Description";
            wsIn.Cell(1, 5).Value = "Amount";
            wsIn.Cell(1, 6).Value = "Date";
            wsIn.Cell(1, 7).Value = "ReportId";

            var row = 2;
            foreach (var inc in incomes)
            {
                wsIn.Cell(row, 1).Value = inc.Id.ToString();
                wsIn.Cell(row, 2).Value = inc.ModuleId.ToString();
                wsIn.Cell(row, 3).Value = inc.IncomeType;
                wsIn.Cell(row, 4).Value = inc.Description;
                wsIn.Cell(row, 5).Value = Convert.ToDouble(inc.Amount);
                wsIn.Cell(row, 6).Value = inc.Date;
                wsIn.Cell(row, 7).Value = inc.ReportId?.ToString();
                row++;
            }

            wsIn.Columns().AdjustToContents();

            // Hoja de Gastos
            var wsEx = workbook.Worksheets.Add("Expenses");
            wsEx.Cell(1, 1).Value = "Id";
            wsEx.Cell(1, 2).Value = "ModuleId";
            wsEx.Cell(1, 3).Value = "ExpenseType";
            wsEx.Cell(1, 4).Value = "Description";
            wsEx.Cell(1, 5).Value = "Amount";
            wsEx.Cell(1, 6).Value = "Date";
            wsEx.Cell(1, 7).Value = "ReportId";

            row = 2;
            foreach (var ex in expenses)
            {
                wsEx.Cell(row, 1).Value = ex.Id.ToString();
                wsEx.Cell(row, 2).Value = ex.ModuleId.ToString();
                wsEx.Cell(row, 3).Value = ex.ExpenseType;
                wsEx.Cell(row, 4).Value = ex.Description;
                wsEx.Cell(row, 5).Value = Convert.ToDouble(ex.Amount);
                wsEx.Cell(row, 6).Value = ex.Date;
                wsEx.Cell(row, 7).Value = ex.ReportId?.ToString();
                row++;
            }

            wsEx.Columns().AdjustToContents();

            // Opcional: Hoja resumen
            var wsSum = workbook.Worksheets.Add("Summary");
            wsSum.Cell(1, 1).Value = "StartDate";
            wsSum.Cell(1, 2).Value = start;
            wsSum.Cell(2, 1).Value = "EndDate";
            wsSum.Cell(2, 2).Value = end;
            wsSum.Cell(4, 1).Value = "TotalIncome";
            wsSum.Cell(4, 2).Value = incomes?.Sum(i => i.Amount) ?? 0;
            wsSum.Cell(5, 1).Value = "TotalExpenses";
            wsSum.Cell(5, 2).Value = expenses?.Sum(e => e.Amount) ?? 0;
            wsSum.Cell(6, 1).Value = "NetProfit";
            wsSum.Cell(6, 2).Value = (incomes?.Sum(i => i.Amount) ?? 0) - (expenses?.Sum(e => e.Amount) ?? 0);
            wsSum.Columns().AdjustToContents();

            // Generar stream
            using var ms = new MemoryStream();
            workbook.SaveAs(ms);
            ms.Position = 0;

            var fileName = $"FinancialReport_{start:yyyyMMdd}_{end:yyyyMMdd}.xlsx";
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}
