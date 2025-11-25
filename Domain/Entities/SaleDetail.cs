using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class SaleDetail
{
    public Guid Id { get; set; }

    public Guid SaleId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public double Mount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Sale Sale { get; set; } = null!;
}
