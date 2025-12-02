namespace Application.DTOs.Inventory;

/// <summary>
/// DTO para transferir datos de Category
/// </summary>
public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int ProductCount { get; set; }
}

/// <summary>
/// DTO para crear una nueva categoría
/// </summary>
public class CreateCategoryDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

/// <summary>
/// DTO para actualizar una categoría
/// </summary>
public class UpdateCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool Status { get; set; }
}
