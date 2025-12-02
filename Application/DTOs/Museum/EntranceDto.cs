namespace Application.DTOs.Museum;

// DTOs para la entidad Entrance
public class EntranceDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid TypePersonId { get; set; }
    public string SaleDate { get; set; } = null!;
    public int Cantidad { get; set; }
    public string SaleNumber { get; set; } = null!;
    public Guid SaleChannel { get; set; }
    public double TotalSale { get; set; }
    public Guid PaymentMethod { get; set; }
    public bool Free { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Propiedades de navegación
    public string? UserName { get; set; }
    public string? TypePersonName { get; set; }
    public string? SaleChannelName { get; set; }
    public string? PaymentMethodName { get; set; }
}

// DTOs para crear y actualizar Entrance
public class CreateEntranceDto
{
    public Guid UserId { get; set; }
    public Guid TypePersonId { get; set; }
    public string SaleDate { get; set; } = null!;
    public int Cantidad { get; set; }
    public string SaleNumber { get; set; } = null!;
    public Guid SaleChannel { get; set; }
    public double TotalSale { get; set; }
    public Guid PaymentMethod { get; set; }
    public bool Free { get; set; }
}

// DTO para actualizar Entrance
public class UpdateEntranceDto
{
    public Guid? UserId { get; set; }
    public Guid? TypePersonId { get; set; }
    public string? SaleDate { get; set; }
    public int? Cantidad { get; set; }
    public string? SaleNumber { get; set; }
    public Guid? SaleChannel { get; set; }
    public double? TotalSale { get; set; }
    public Guid? PaymentMethod { get; set; }
    public bool? Free { get; set; }
}
