using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MonasteryExpense
{
    public Guid Id { get; set; }

    public string Category { get; set; } = null!;

    public double Amount { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Descripción { get; set; } = null!;

    public Guid? OverheadsId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Overhead? Overheads { get; set; }
}
