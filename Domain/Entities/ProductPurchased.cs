using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ProductPurchased
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid? BuysproductId { get; set; }

    public Guid? ProductPurchasedId { get; set; }

    public virtual BuysProduct? Buysproduct { get; set; }

    public virtual BuysProduct? ProductPurchasedNavigation { get; set; }
}
