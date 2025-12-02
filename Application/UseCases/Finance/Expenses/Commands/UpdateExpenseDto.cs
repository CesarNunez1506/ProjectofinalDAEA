using System;

namespace Application.UseCases.Finance.Expenses.Commands
{
    public class UpdateExpenseDto
    {
        public string? Description { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
    }
}
