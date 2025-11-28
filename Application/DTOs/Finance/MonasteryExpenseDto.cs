using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Finance;

/// <summary>
/// DTO para crear un nuevo gasto del monasterio
/// </summary>
public class CreateMonasteryExpenseDto
{
    [Required(ErrorMessage = "La categoría es requerida")]
    [StringLength(100, ErrorMessage = "La categoría no debe exceder los 100 caracteres")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "El monto es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
    public double Amount { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(255, ErrorMessage = "El nombre no debe exceder los 255 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha es requerida")]
    public DateTime Date { get; set; }

    [StringLength(500, ErrorMessage = "La descripción no debe exceder los 500 caracteres")]
    public string? Descripcion { get; set; }
}

/// <summary>
/// DTO para actualizar un gasto del monasterio
/// </summary>
public class UpdateMonasteryExpenseDto
{
    [StringLength(100, ErrorMessage = "La categoría no debe exceder los 100 caracteres")]
    public string? Category { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
    public double? Amount { get; set; }

    [StringLength(255, ErrorMessage = "El nombre no debe exceder los 255 caracteres")]
    public string? Name { get; set; }

    public DateTime? Date { get; set; }

    [StringLength(500, ErrorMessage = "La descripción no debe exceder los 500 caracteres")]
    public string? Descripcion { get; set; }
}

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
