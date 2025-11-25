using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class GeneralIncome
{
    public Guid Id { get; set; }

    public Guid ModuleId { get; set; }

    public string IncomeType { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string? Description { get; set; }

    public Guid? ReportId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Module Module { get; set; } = null!;

    public virtual FinancialReport? Report { get; set; }
}
