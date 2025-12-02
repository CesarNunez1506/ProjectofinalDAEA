namespace Application.DTOs.Inventory;

/// <summary>
/// DTO para transferir datos de Supplier
/// </summary>
public class SupplierDto
{
    public Guid Id { get; set; }
    public long Ruc { get; set; }
    public string SuplierName { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public long Phone { get; set; }
    public string Address { get; set; } = null!;
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// DTO para crear un nuevo proveedor
/// </summary>
public class CreateSupplierDto
{
    public long Ruc { get; set; }
    public string SuplierName { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public long Phone { get; set; }
    public string Address { get; set; } = null!;
}

/// <summary>
/// DTO para actualizar un proveedor
/// </summary>
public class UpdateSupplierDto
{
    public Guid Id { get; set; }
    public long Ruc { get; set; }
    public string SuplierName { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public long Phone { get; set; }
    public string Address { get; set; } = null!;
    public bool Status { get; set; }
}
