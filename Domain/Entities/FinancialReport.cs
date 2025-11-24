using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class FinancialReport
{
    public Guid Id { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal TotalIncome { get; set; }

    public decimal TotalExpenses { get; set; }

    public decimal NetProfit { get; set; }

    public string? Observations { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<GeneralExpense> GeneralExpenses { get; set; } = new List<GeneralExpense>();

    public virtual ICollection<GeneralIncome> GeneralIncomes { get; set; } = new List<GeneralIncome>();
}
