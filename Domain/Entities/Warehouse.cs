using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class Warehouse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int Capacity { get; set; }

    public string? Observation { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<BuysProduct> BuysProducts { get; set; } = new List<BuysProduct>();

    public virtual ICollection<WarehouseMovementProduct> WarehouseMovementProducts { get; set; } = new List<WarehouseMovementProduct>();

    public virtual ICollection<WarehouseMovementResource> WarehouseMovementResources { get; set; } = new List<WarehouseMovementResource>();

    public virtual ICollection<WarehouseProduct> WarehouseProducts { get; set; } = new List<WarehouseProduct>();

    public virtual ICollection<WarehouseResource> WarehouseResources { get; set; } = new List<WarehouseResource>();
}
