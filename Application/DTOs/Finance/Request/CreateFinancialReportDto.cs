using System;

namespace Application.DTOs.Finance.Request
{
    public class CreateFinancialReportDto
    {
        public DateTime ReportDate { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalGeneralExpenses { get; set; }
        public decimal TotalMonasteryExpenses { get; set; }
        public decimal TotalOverheads { get; set; }
        public decimal NetBalance { get; set; }
        public string? Notes { get; set; }
    }
}
