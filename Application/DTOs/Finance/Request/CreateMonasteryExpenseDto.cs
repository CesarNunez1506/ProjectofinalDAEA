using System;

namespace Application.DTOs.Finance.Request
{
    public class CreateMonasteryExpenseDto
    {
        public Guid ModuleId { get; set; }
        public string ExpenseType { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid? ReportId { get; set; }
    }
}
