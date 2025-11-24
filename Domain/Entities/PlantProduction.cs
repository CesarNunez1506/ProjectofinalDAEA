using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class PlantProduction
{
    public Guid Id { get; set; }

    public string PlantName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public Guid WarehouseId { get; set; }

    public bool Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Production> Productions { get; set; } = new List<Production>();
}
