using System;

namespace Application.DTOs.Finance;

/// <summary>
/// DTO de respuesta para gasto del monasterio
/// </summary>
public class MonasteryExpenseDto
{
    public Guid Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public double Amount { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string? Descripcion { get; set; }
    public Guid? OverheadsId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
