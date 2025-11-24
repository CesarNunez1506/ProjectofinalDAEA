using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class RolesPermission
{
    public Guid RoleId { get; set; }

    public Guid PermissionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
