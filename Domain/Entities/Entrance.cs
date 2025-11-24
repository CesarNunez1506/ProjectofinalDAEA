using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Entrance
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid TypePersonId { get; set; }

    public string SaleDate { get; set; } = null!;

    public int Cantidad { get; set; }

    public string SaleNumber { get; set; } = null!;

    public Guid SaleChannel { get; set; }

    public double TotalSale { get; set; }

    public Guid PaymentMethod { get; set; }

    public bool Free { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual PaymentMethod PaymentMethodNavigation { get; set; } = null!;

    public virtual SalesChannel SaleChannelNavigation { get; set; } = null!;

    public virtual TypePerson TypePerson { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
