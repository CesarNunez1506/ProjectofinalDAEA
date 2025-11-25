using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class Supplier
{
    public Guid Id { get; set; }

    public long Ruc { get; set; }

    public string SuplierName { get; set; } = null!;

    public string ContactName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long Phone { get; set; }

    public string Address { get; set; } = null!;

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<BuysProduct> BuysProducts { get; set; } = new List<BuysProduct>();

    public virtual ICollection<Resource> Resources { get; set; } = new List<Resource>();

    public virtual ICollection<WarehouseResource> WarehouseResources { get; set; } = new List<WarehouseResource>();
}
