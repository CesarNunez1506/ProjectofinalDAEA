using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class Rental
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Guid PlaceId { get; set; }

    public Guid UserId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal Amount { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Place Place { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
