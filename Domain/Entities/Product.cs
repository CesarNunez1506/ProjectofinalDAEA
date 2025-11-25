using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid CategoryId { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public string? ImagenUrl { get; set; }

    public bool Status { get; set; }

    public bool Producible { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<BuysProduct> BuysProducts { get; set; } = new List<BuysProduct>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Production> Productions { get; set; } = new List<Production>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual ICollection<Return> Returns { get; set; } = new List<Return>();

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();

    public virtual ICollection<WarehouseMovementProduct> WarehouseMovementProducts { get; set; } = new List<WarehouseMovementProduct>();

    public virtual ICollection<WarehouseProduct> WarehouseProducts { get; set; } = new List<WarehouseProduct>();

    public virtual ICollection<WarehouseStore> WarehouseStores { get; set; } = new List<WarehouseStore>();
}
