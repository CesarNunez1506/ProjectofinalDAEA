using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Store
{
    public Guid Id { get; set; }

    public string StoreName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Observations { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Return> Returns { get; set; } = new List<Return>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<WarehouseStore> WarehouseStores { get; set; } = new List<WarehouseStore>();
}
