using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Location
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int Capacity { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Place> Places { get; set; } = new List<Place>();
}
