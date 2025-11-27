using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class WarehouseResource
{
    public Guid Id { get; set; }

    public Guid WarehouseId { get; set; }

    public Guid ResourceId { get; set; }

    public double Quantity { get; set; }

    public string TypeUnit { get; set; } = null!;

    public double UnitPrice { get; set; }

    public double TotalCost { get; set; }

    public Guid SupplierId { get; set; }

    public DateTime EntryDate { get; set; }

    public bool? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Resource Resource { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;
}
