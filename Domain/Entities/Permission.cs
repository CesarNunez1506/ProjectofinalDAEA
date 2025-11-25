using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class Permission
{
    public Guid Id { get; set; }

    public Guid ModuleId { get; set; }

    public bool? CanRead { get; set; }

    public bool? CanWrite { get; set; }

    public bool? CanEdit { get; set; }

    public bool? CanDelete { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Module Module { get; set; } = null!;

    public virtual ICollection<RolesPermission> RolesPermissions { get; set; } = new List<RolesPermission>();
}
