using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Finance.Request
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
