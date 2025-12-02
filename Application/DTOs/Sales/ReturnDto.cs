namespace Application.DTOs.Sales;

/// <summary>
/// DTO para crear una devolución o pérdida
/// </summary>
public class CreateReturnDto
{
    public Guid? ProductId { get; set; }
    public Guid? SalesId { get; set; } // Null = pérdida directa
    public Guid? StoreId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string? Observations { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

/// <summary>
/// DTO para actualizar una devolución
/// </summary>
public class UpdateReturnDto
{
    public Guid? ProductId { get; set; }
    public Guid? SalesId { get; set; }
    public Guid? StoreId { get; set; }
    public string? Reason { get; set; }
    public string? Observations { get; set; }
    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
}

/// <summary>
/// DTO de respuesta para devolución
/// </summary>
public class ReturnDto
{
    public Guid Id { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? SalesId { get; set; }
    public Guid? StoreId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string? Observations { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
