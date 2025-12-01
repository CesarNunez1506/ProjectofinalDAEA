namespace Application.DTOs.Sales;

public class SaleDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public decimal Tax { get; set; }
    public string Status { get; set; } = string.Empty;

    public List<SaleDetailDto> Details { get; set; } = new();
}
