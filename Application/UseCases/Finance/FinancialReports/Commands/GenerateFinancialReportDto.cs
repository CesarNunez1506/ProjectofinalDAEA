using System;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Finance.FinancialReports.Commands
{
    public class GenerateFinancialReportDto
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string? Observations { get; set; }
    }
}
