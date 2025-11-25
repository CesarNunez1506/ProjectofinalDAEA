using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class WarehouseMovementResource
{
    public Guid Id { get; set; }

    public Guid WarehouseId { get; set; }

    public Guid ResourceId { get; set; }

    public string MovementType { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime MovementDate { get; set; }

    public string? Observations { get; set; }

    public bool? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Resource Resource { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;
}
