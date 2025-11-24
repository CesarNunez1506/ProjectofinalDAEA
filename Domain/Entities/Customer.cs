using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class Customer
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = null!;

    public int Dni { get; set; }

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
