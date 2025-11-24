using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class IncomeChurch
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public double Price { get; set; }

    public bool Status { get; set; }

    public string Date { get; set; } = null!;

    public Guid IdChurch { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Church IdChurchNavigation { get; set; } = null!;
}
