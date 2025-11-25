using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class CashSession
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = null!;

    public string StoreId { get; set; } = null!;

    public decimal StartAmount { get; set; }

    public decimal? EndAmount { get; set; }

    public decimal? TotalSales { get; set; }

    public decimal? TotalReturns { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime? EndedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
