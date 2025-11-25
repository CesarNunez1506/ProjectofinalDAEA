using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phonenumber { get; set; } = null!;

    public string Dni { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Guid? RoleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Entrance> Entrances { get; set; } = new List<Entrance>();

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    public virtual Role? Role { get; set; }
}
