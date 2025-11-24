using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Lost
{
    public Guid Id { get; set; }

    public Guid ProductionId { get; set; }

    public int Quantity { get; set; }

    public string LostType { get; set; } = null!;

    public string? Observations { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime CreatedAt1 { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Production Production { get; set; } = null!;
}
