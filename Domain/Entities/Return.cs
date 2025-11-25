using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class Return
{
    public Guid Id { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? SalesId { get; set; }

    public Guid? StoreId { get; set; }

    public string Reason { get; set; } = null!;

    public string? Observations { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Sale? Sales { get; set; }

    public virtual Store? Store { get; set; }
}
