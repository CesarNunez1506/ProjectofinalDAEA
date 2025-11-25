using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class BuysProduct
{
    public Guid Id { get; set; }

    public Guid WarehouseId { get; set; }

    public Guid ProductId { get; set; }

    public double Quantity { get; set; }

    public double UnitPrice { get; set; }

    public double TotalCost { get; set; }

    public Guid SupplierId { get; set; }

    public DateTime EntryDate { get; set; }

    public bool? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ProductPurchased> ProductPurchasedBuysproducts { get; set; } = new List<ProductPurchased>();

    public virtual ICollection<ProductPurchased> ProductPurchasedProductPurchasedNavigations { get; set; } = new List<ProductPurchased>();

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;
}
