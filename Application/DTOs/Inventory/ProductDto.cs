namespace Application.DTOs.Inventory;

/// <summary>
/// DTO para transferir datos de Product
/// </summary>
public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? ImagenUrl { get; set; }
    public bool Status { get; set; }
    public bool Producible { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// DTO para crear un nuevo producto
/// </summary>
public class CreateProductDto
{
    public string Name { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? ImagenUrl { get; set; }
    public bool Producible { get; set; }
}

/// <summary>
/// DTO para actualizar un producto
/// </summary>
public class UpdateProductDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid? CategoryId { get; set; }
    public decimal? Price { get; set; }
    public string? Description { get; set; }
    public string? ImagenUrl { get; set; }
    public bool? Status { get; set; }
    public bool? Producible { get; set; }
}
