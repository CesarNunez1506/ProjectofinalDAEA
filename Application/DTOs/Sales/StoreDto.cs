namespace Application.DTOs.Sales;

/// <summary>
/// DTO para crear una nueva tienda
/// </summary>
public class CreateStoreDto
{
    public string StoreName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Observations { get; set; }
}

/// <summary>
/// DTO para actualizar una tienda existente
/// </summary>
public class UpdateStoreDto
{
    public string StoreName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Observations { get; set; }
}

/// <summary>
/// DTO de respuesta para tienda
/// </summary>
public class StoreDto
{
    public Guid Id { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Observations { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
