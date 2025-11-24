using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class Church
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public bool State { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<IncomeChurch> IncomeChurches { get; set; } = new List<IncomeChurch>();

    public virtual ICollection<RentChurch> RentChurches { get; set; } = new List<RentChurch>();
}
