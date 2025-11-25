using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class Sale
{
    public Guid Id { get; set; }

    public string IncomeDate { get; set; } = null!;

    public Guid StoreId { get; set; }

    public double TotalIncome { get; set; }

    public string? Observations { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Return> Returns { get; set; } = new List<Return>();

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();

    public virtual Store Store { get; set; } = null!;
}
