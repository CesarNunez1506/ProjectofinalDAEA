using System;

namespace Application.DTOs.Finance.Request
{
    public class CreateIncomeDto
    {
        public Guid ModuleId { get; set; }
        public string IncomeType { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid? ReportId { get; set; }
    }
}
