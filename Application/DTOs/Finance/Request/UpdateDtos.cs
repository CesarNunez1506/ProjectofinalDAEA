using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Finance.Request
{
    public class UpdateIncomeDto
    {
        public string? Description { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
    }

    public class UpdateExpenseDto
    {
        public string? Description { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
    }

    public class UpdateOverheadDto
    {
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
    }

    public class UpdateMonasteryExpenseDto
    {
        public string? Category { get; set; }
        public double? Amount { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public string? Descripcion { get; set; }
    }

    public class UpdateFinancialReportDto
    {
        public string? Notes { get; set; }
    }
}
