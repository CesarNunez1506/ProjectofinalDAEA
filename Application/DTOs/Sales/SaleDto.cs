namespace Application.DTOs.Sales;

/// <summary>
/// DTO para crear una nueva venta
/// </summary>
public class CreateSaleDto
{
    public string IncomeDate { get; set; } = string.Empty;
    public Guid StoreId { get; set; }
    public double TotalIncome { get; set; }
    public string? Observations { get; set; }
}

/// <summary>
/// DTO para actualizar una venta
/// </summary>
public class UpdateSaleDto
{
    public string IncomeDate { get; set; } = string.Empty;
    public Guid StoreId { get; set; }
    public double TotalIncome { get; set; }
    public string? Observations { get; set; }
}

/// <summary>
/// DTO de respuesta para venta
/// </summary>
public class SaleDto
{
    public Guid Id { get; set; }
    public string IncomeDate { get; set; } = string.Empty;
    public Guid StoreId { get; set; }
    public double TotalIncome { get; set; }
    public string? Observations { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
