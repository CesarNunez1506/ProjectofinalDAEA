namespace Application.DTOs.Sales;

/// <summary>
/// DTO para crear un nuevo canal de ventas
/// </summary>
public class CreateSalesChannelDto
{
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// DTO para actualizar un canal de ventas
/// </summary>
public class UpdateSalesChannelDto
{
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// DTO de respuesta para canal de ventas
/// </summary>
public class SalesChannelDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
