using System;

namespace Application.DTOs.Finance.Request
{
    public class GenerateFinancialReportDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Notes { get; set; }
        public string? Observations { get; set; }
    }
}
