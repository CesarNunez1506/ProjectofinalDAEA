using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Finance
{
    public class MonasteryExpenseDto
    {
        public Guid Id { get; set; }
        public Guid ModuleId { get; set; }
        public string ExpenseType { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid? ReportId { get; set; }
    }
}
