namespace Application.DTOs.Museum;

// DTO para la entidad SalesChannel (Canal de Venta)
public class SalesChannelDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

// DTO para crear SalesChannel
public class CreateSalesChannelDto
{
    public string Name { get; set; } = null!;
}

// DTO para actualizar SalesChannel
public class UpdateSalesChannelDto
{
    public string? Name { get; set; }
}
