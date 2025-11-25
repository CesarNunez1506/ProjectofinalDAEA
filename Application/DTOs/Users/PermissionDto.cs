namespace Application.DTOs.Users;

public class PermissionDto
{
    public Guid Id { get; set; }
    public Guid ModuleId { get; set; }
    public string? ModuleName { get; set; }
    public bool? CanRead { get; set; }
    public bool? CanWrite { get; set; }
    public bool? CanEdit { get; set; }
    public bool? CanDelete { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreatePermissionDto
{
    public Guid ModuleId { get; set; }
    public bool? CanRead { get; set; }
    public bool? CanWrite { get; set; }
    public bool? CanEdit { get; set; }
    public bool? CanDelete { get; set; }
}

public class UpdatePermissionDto
{
    public bool? CanRead { get; set; }
    public bool? CanWrite { get; set; }
    public bool? CanEdit { get; set; }
    public bool? CanDelete { get; set; }
}
