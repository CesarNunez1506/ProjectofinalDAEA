namespace Application.DTOs.Sales;

/// <summary>
/// DTO para crear un detalle de venta
/// </summary>
public class CreateSaleDetailDto
{
    public Guid SaleId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public double Mount { get; set; }
}

/// <summary>
/// DTO para actualizar un detalle de venta
/// </summary>
public class UpdateSaleDetailDto
{
    public Guid SaleId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public double Mount { get; set; }
}

/// <summary>
/// DTO de respuesta para detalle de venta
/// </summary>
public class SaleDetailDto
{
    public Guid Id { get; set; }
    public Guid SaleId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public double Mount { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
