using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class RentChurch
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string StartTime { get; set; } = null!;

    public string EndTime { get; set; } = null!;

    public double Price { get; set; }

    public bool Status { get; set; }

    public DateOnly Date { get; set; }

    public Guid? IdChurch { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Church? IdChurchNavigation { get; set; }
}
