using System;

namespace Application.UseCases.Finance.Incomes.Commands
{
    public class UpdateIncomeDto
    {
        public string? Description { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
    }
}
