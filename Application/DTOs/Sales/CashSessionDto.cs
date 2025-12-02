namespace Application.DTOs.Sales;

/// <summary>
/// DTO para crear una sesión de caja
/// </summary>
public class CreateCashSessionDto
{
    public string StoreId { get; set; } = string.Empty;
    public decimal StartAmount { get; set; }
}

/// <summary>
/// DTO para actualizar/cerrar una sesión de caja
/// </summary>
public class UpdateCashSessionDto
{
    public decimal? EndAmount { get; set; }
    public decimal? TotalSales { get; set; }
    public decimal? TotalReturns { get; set; }
    public DateTime? EndedAt { get; set; }
}

/// <summary>
/// DTO de respuesta para sesión de caja
/// </summary>
public class CashSessionDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string StoreId { get; set; } = string.Empty;
    public decimal StartAmount { get; set; }
    public decimal? EndAmount { get; set; }
    public decimal? TotalSales { get; set; }
    public decimal? TotalReturns { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
