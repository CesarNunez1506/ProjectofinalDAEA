using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class Resource
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Observation { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid? SupplierId { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual Supplier? Supplier { get; set; }

    public virtual ICollection<WarehouseMovementResource> WarehouseMovementResources { get; set; } = new List<WarehouseMovementResource>();

    public virtual ICollection<WarehouseResource> WarehouseResources { get; set; } = new List<WarehouseResource>();
}
