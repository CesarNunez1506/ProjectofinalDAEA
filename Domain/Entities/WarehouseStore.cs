using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class WarehouseStore
{
    public Guid Id { get; set; }

    public Guid StoreId { get; set; }

    public Guid ProductId { get; set; }

    public double Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
