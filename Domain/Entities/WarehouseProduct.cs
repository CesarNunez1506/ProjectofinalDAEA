using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class WarehouseProduct
{
    public Guid Id { get; set; }

    public Guid WarehouseId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime EntryDate { get; set; }

    public bool? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;
}
