using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class Production
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public int QuantityProduced { get; set; }

    public DateTime ProductionDate { get; set; }

    public string? Observation { get; set; }

    public Guid PlantId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Lost> Losts { get; set; } = new List<Lost>();

    public virtual PlantProduction Plant { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
